using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using TriviaGameRestEFAPI.BLL.DataContract;
using TriviaGameRestEFAPI.Data;
using TriviaGameRestEFAPI.Data.Entities;

namespace TriviaGameRestEFAPI.BLL
{
    public static class TriviaGameRestEFAPIBLL
    {
        private static TriviaGameDBContext DBContext
        {
            get
            {
                return new TriviaGameDBContext();
            }
        }

        public static void InitialiseConnectionString(IConfiguration configuration)
        {
            try
            {
                if (configuration != null)
                {
                    if (string.IsNullOrWhiteSpace(Utilities.DatabaseHelper.ConnectionString))
                    {
                        Utilities.DatabaseHelper.ConnectionString = configuration.GetConnectionString("DatabasePath");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static class GenreHelper
        {
            public static async Task<List<GenreResp>> GetGenres()
            {
                try
                {
                    using TriviaGameDBContext _dbContext = DBContext;

                    List<GenreResp> _genreResps = new List<GenreResp>();

                    foreach (var genre in await TriviaGameRestEFAPIDAL.GetGenres(_dbContext))
                    {
                        _genreResps.Add(FillGenreResp(genre));
                    }

                    return _genreResps;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static async Task UpdateGenre(Guid genreId,  string genreName)
            {
                try
                {
                    using TriviaGameDBContext _dbContext = DBContext;

                    Genre _genre = await TriviaGameRestEFAPIDAL.GetGenreById(_dbContext, genreId);
                    _genre.GenreName = genreName;

                    EntityEntry _entityEntry = _dbContext.Update(_genre);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static async Task JoinDB()
            {
                try
                {
                    using TriviaGameDBContext _dbContext = DBContext;

                    List<Genre> _genres = await TriviaGameRestEFAPIDAL.JoinDB(_dbContext);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static GenreResp FillGenreResp(Genre genre)
            {
                try
                {
                    if (genre != null)
                    {
                        return new GenreResp()
                        {
                            GenreName = genre.GenreName
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static class GameHelper
        {
            public static async Task<GameResp> CreateGame(string genreName)
            {
                try
                {
                    using TriviaGameDBContext _dbContext = DBContext;

                    Genre _genre = await TriviaGameRestEFAPIDAL.GetGenreByGenreName(_dbContext, genreName);

                    if (_genre is null)
                    {
                        throw new Exception("Invalid Genre Name.");
                    }

                    Game _game = new Game()
                    {
                        Genre = _genre,
                        CreationDate = DateTime.Now,
                    };

                    await _dbContext.Game.AddAsync(_game);
                    await _dbContext.SaveChangesAsync();

                    return FillGameResp(_game);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            public static async Task<bool> AnswerQuestion(AnswerQuestionReq answerQuestionReq)
            {
                try
                {
                    using TriviaGameDBContext _dbContext = DBContext;

                    Game _game = await TriviaGameRestEFAPIDAL.GetGameById(_dbContext, answerQuestionReq.GameId);

                    if (_game is null)
                    {
                        throw new Exception("Invalid Game Id.");
                    }

                    Question _question = await TriviaGameRestEFAPIDAL.GetQuestionById(_dbContext, answerQuestionReq.QuestionId);

                    if (_question is null)
                    {
                        throw new Exception("Invalid Question Id.");
                    }

                    if (await TriviaGameRestEFAPIDAL.IsQuestionAnswered(_dbContext, answerQuestionReq.GameId, answerQuestionReq.QuestionId))
                    {
                        throw new Exception("Question already answered.");
                    }

                    Choice _choice = null;

                    if (answerQuestionReq.ChoiceId != null)
                    {
                        _choice = await TriviaGameRestEFAPIDAL.GetChoiceById(_dbContext, (Guid)answerQuestionReq.ChoiceId);

                        if (_choice is null)
                        {
                            throw new Exception("Invalid Choice Id.");
                        }
                    }

                    answerQuestionReq.AnswerDuration = answerQuestionReq.AnswerDuration > _question.QuestionDuration ?
                                                                                          _question.QuestionDuration :
                                                                                           answerQuestionReq.AnswerDuration;

                    Answer _answer = new Answer()
                    {
                        Game = _game,
                        Question = _question,
                        Choice = _choice,
                        AnswerDuration = answerQuestionReq.ChoiceId != null ? answerQuestionReq.AnswerDuration : _question.QuestionDuration
                    };

                    await _dbContext.Answer.AddAsync(_answer);
                    await _dbContext.SaveChangesAsync();

                    return !(_choice is null) && _choice.IsCorrect;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static async Task<GameResultResp> ViewGame(Guid gameId)
            {
                try
                {
                    using TriviaGameDBContext _dbContext = DBContext;

                    Game _game = await TriviaGameRestEFAPIDAL.GetGameById(_dbContext, gameId);

                    if (_game is null)
                    {
                        throw new Exception("Invalid Game Id.");
                    }

                    return FillGameResultResp(_game);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static GameResp FillGameResp(Game game)
            {
                try
                {
                    if (game != null)
                    {
                        GameResp _gameResp = new GameResp()
                        {
                            GameId = game.GameId,
                            CreationDate = game.CreationDate.ToString("dd-MMMM-yyyy hh:mm:ss tt"),
                            GenreName = game.GenreName
                        };

                        foreach (var question in game.Questions)
                        {
                            _gameResp.Questions.Add(FillQuestionResp(question));
                        }

                        return _gameResp;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static QuestionResp FillQuestionResp(Question question)
            {
                try
                {
                    if (question != null)
                    {
                        QuestionResp _questionResp = new QuestionResp()
                        {
                            QuestionId = question.QuestionId,
                            QuestionNumber = question.QuestionNumber,
                            QuestionDescription = question.QuestionDescription,
                            QuestionDuration = question.QuestionDuration
                        };

                        foreach (var choice in question.Choice)
                        {
                            _questionResp.Choices.Add(FillChoiceResp(choice));
                        }

                        return _questionResp;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static ChoiceResp FillChoiceResp(Choice choice)
            {
                try
                {
                    if (choice != null)
                    {
                        return new ChoiceResp()
                        {
                            ChoiceId = choice.ChoiceId,
                            ChoiceName = choice.ChoiceName,
                            IsCorrect = choice.IsCorrect
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static GameResultResp FillGameResultResp(Game game)
            {
                try
                {
                    if (game != null)
                    {
                        GameResultResp _gameResultResp = new GameResultResp()
                        {
                            GameId = game.GameId,
                            GenreName = game.GenreName,
                            CreationDate = game.CreationDate.ToString("dd-MMMM-yyyy hh:mm:ss tt"),
                            TotalScore = game.TotalScore
                        };

                        foreach (var answer in game.Answer)
                        {
                            _gameResultResp.Answers.Add(FillAnswerResp(answer));
                        }

                        return _gameResultResp;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static AnswerResp FillAnswerResp(Answer answer)
            {
                try
                {
                    if (answer != null)
                    {
                        return new AnswerResp()
                        {
                            QuestionDescription = answer.QuestionDescription,
                            QuestionNumber = answer.QuestionNumber,
                            QuestionDuration = answer.QuestionDuration,
                            AnswerDescription = answer.AnswerDescription,
                            AnswerDuration = answer.AnswerDuration,
                            IsAnswerCorrect = answer.IsAnswerCorrect,
                            IsQuestionAnswered = answer.IsQuestionAnswered,
                            Score = answer.Score
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
