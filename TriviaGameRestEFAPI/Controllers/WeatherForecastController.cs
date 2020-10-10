using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TriviaGameRestEFAPI.BLL;
using TriviaGameRestEFAPI.BLL.DataContract;

namespace TriviaGameRestEFAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            try
            {
                await TriviaGameRestEFAPIBLL.GenreHelper.JoinDB();

                //foreach (var genre in await TriviaGameRestEFAPIBLL.GenreHelper.GetGenres())
                //{
                //    //await TriviaGameRestEFAPIBLL.GenreHelper.UpdateGenre(genre.GenreName);

                //    GameResp _gameResp = await TriviaGameRestEFAPIBLL.GameHelper.CreateGame(genre.GenreName);

                //    foreach (var question in _gameResp.Questions)
                //    {
                //        await TriviaGameRestEFAPIBLL.GameHelper.AnswerQuestion(new AnswerQuestionReq()
                //        {
                //            GameId = _gameResp.GameId,
                //            QuestionId = question.QuestionId,
                //            ChoiceId = question.Choices[3].ChoiceId,
                //            AnswerDuration = 10
                //        });
                //    }

                //    GameResultResp _gameResultResp = await TriviaGameRestEFAPIBLL.GameHelper.ViewGame(_gameResp.GameId);
                //}
            }
            catch (Exception)
            {
                throw;
            }

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
