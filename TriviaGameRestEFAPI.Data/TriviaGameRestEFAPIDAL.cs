using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TriviaGameRestEFAPI.Data.Entities;

namespace TriviaGameRestEFAPI.Data
{
    public static class TriviaGameRestEFAPIDAL
    {
        public static async Task<Game> GetGameById(TriviaGameDBContext dbContext, Guid gameId)
        {
            try
            {
                return await dbContext.Game.FirstOrDefaultAsync(item => item.GameId == gameId);
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
                return await dbContext.Question.
                             FirstOrDefaultAsync(item => item.QuestionId == questionId);
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
                return await dbContext.Genre.
                             FirstOrDefaultAsync(item => item.GenreName == genreName);
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
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Choice> GetChoiceById(TriviaGameDBContext dbContext, Guid choiceId)
        {
            try
            {
                return await dbContext.Choice.
                             FirstOrDefaultAsync(item => item.ChoiceId == choiceId);
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

                return await dbContext.Answer.
                             Where(item => item.Game == _game &&
                                           item.Question == _question).
                                           CountAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
