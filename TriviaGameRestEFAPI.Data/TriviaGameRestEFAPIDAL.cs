using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using TriviaGameRestEFAPI.Data.Entities;

namespace TriviaGameRestEFAPI.Data
{
    public static class TriviaGameRestEFAPIDAL
    {
        public static async Task<Genre> GetGenreById(TriviaGameDBContext dbContext, Guid genreId)
        {
            try
            {
                return await (from genre in dbContext.Genre
                              where genre.GenreId == genreId
                              select genre).
                              FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Genre> GetGenreByGenreName(TriviaGameDBContext dbContext, string genreName)
        {
            try
            {
                return await (from genre in dbContext.Genre
                              where genre.GenreName == genreName
                              select genre).
                              FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Genre>> GetGenres(TriviaGameDBContext dbContext)
        {
            try
            {
                return await dbContext.Genre.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<Game> GetGameById(TriviaGameDBContext dbContext, Guid gameId)
        {
            try
            {
                return await (from game in dbContext.Game
                              where game.GameId == gameId
                              select game).
                              FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Question> GetQuestionById(TriviaGameDBContext dbContext, Guid questionId)
        {
            try
            {
                return await (from question in dbContext.Question
                              where question.QuestionId == questionId
                              select question).
                              FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Choice> GetChoiceById(TriviaGameDBContext dbContext, Guid choiceId)
        {
            try
            {
                return await (from choice in dbContext.Choice
                              where choice.ChoiceId == choiceId
                              select choice).
                              FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> IsQuestionAnswered(TriviaGameDBContext dbContext, Guid gameId, Guid questionId)
        {
            try
            {
                Game _game = await GetGameById(dbContext, gameId);
                Question _question = await GetQuestionById(dbContext, questionId);

                return await (from answer in dbContext.Answer
                              where answer.Game == _game &&
                                    answer.Question == _question
                              select answer).
                              AnyAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Genre>> JoinDB(TriviaGameDBContext dbContext)
        {
            try
            {
                //string _questionDescription = "Who";

                //The following SQL statement selects all Questions with a QuestionDescription that have "_questionDescription" in any position:
                //var _queryLike = await (from question in dbContext.Question
                //                        where EF.Functions.Like(question.QuestionDescription, $"%{_questionDescription}%")
                //                        select question).
                //                        ToListAsync();

                //The following SQL statement selects all Questions with a QuestionDescription starting with "_questionDescription":
                //var _queryLike = await (from question in dbContext.Question
                //                        where EF.Functions.Like(question.QuestionDescription, $"{_questionDescription}%")
                //                        select question).
                //                        ToListAsync();

                //The following SQL statement selects all Questions with a QuestionDescription ending with "_questionDescription":
                //var _queryLike = await (from question in dbContext.Question
                //                        where EF.Functions.Like(question.QuestionDescription, $"%{_questionDescription}")
                //                        select question).
                //                        ToListAsync();


                //The following SQL statement selects all Questions with a QuestionDescription that have "_questionDescription" in the second position:
                //var _queryLike = await (from question in dbContext.Question
                //                        where EF.Functions.Like(question.QuestionDescription, $"_{_questionDescription}%")
                //                        select question).
                //                        ToListAsync();

                //var _queryLike = await (from question in dbContext.Question
                //                        where question.QuestionDescription.Contains(_questionDescription)
                //                        select question).
                //                        ToListAsync();

                //IsIn Operator
                var _questionDescriptions = await (from question in dbContext.Question
                                                   where question.QuestionNumber == 1
                                                   select question.QuestionDescription).
                                                   ToListAsync();

                var _questions = await (from question in dbContext.Question
                                        where !_questionDescriptions.Contains(question.QuestionDescription)
                                        select question).
                                        ToListAsync();


                //var _group = await (from question in dbContext.Question
                //                    join genre in dbContext.Genre
                //                    on question.GenreId equals genre.GenreId
                //                    group question by genre.GenreName into questionG
                //                    select new
                //                    {
                //                        questionG.Key,
                //                        TotalDuration = questionG.Sum(x => x.QuestionDuration)
                //                    }).
                //                    ToListAsync();


                var _subQuery = await (from genre in dbContext.Genre
                                       where genre.GenreId == (from game1 in dbContext.Game
                                                               where game1.CreationDate == (from game2 in dbContext.Game
                                                                                            select game2.CreationDate).
                                                                                            Max()
                                                               select game1.GenreId).
                                                               FirstOrDefault()
                                       select genre).
                                       ToListAsync();


                var _query = from genre in dbContext.Genre
                             join question in dbContext.Question
                             on genre.GenreId equals question.GenreId
                             join answer in dbContext.Answer
                             on question.QuestionId equals answer.QuestionId
                             join choice in dbContext.Choice
                             on answer.ChoiceId equals choice.ChoiceId into leftJoinChoice
                             from choice in leftJoinChoice.DefaultIfEmpty()
                             select new { genre, question, answer, choice };

                if (true)
                {
                    _query = _query.Where(x => x.genre.GenreName == "Action");
                }

                if (true)
                {
                    _query = _query.Where(x => x.question.QuestionDescription == "The movie Gladiator, was directed by whom?");
                }

                if (true)
                {
                    _query = _query.Where(x => x.answer.AnswerDuration > 9);
                }

                if (true)
                {
                    _query = _query.Where(x => x.choice.IsCorrect == true);
                }

                var _result = await _query.Select(x => x.genre).
                                    //Distinct().
                                    ToListAsync();

                //var _result = await dbContext.Genre.
                //                    Join(dbContext.Question,
                //                    genre => genre.GenreId,
                //                    question => question.GenreId,
                //                    (genre, question) => new
                //                    {
                //                        genre,
                //                        question
                //                    }).
                //                    Where(x => x.genre.GenreName == "Action").
                //                    Join(dbContext.Answer,
                //                    question => question.question.QuestionId,
                //                    answer => answer.QuestionId,
                //                    (question, answer) => new
                //                    {
                //                        answer
                //                    }).
                //                    Join(dbContext.Choice,
                //                    answer => answer.answer.ChoiceId,
                //                    choice => choice.ChoiceId,
                //                    (answer, choice) => new { answer.answer.Question.Genre }).
                //                    //Where(x => x.choice.ChoiceName == "China").
                //                    Distinct().
                //                    ToListAsync();

                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
