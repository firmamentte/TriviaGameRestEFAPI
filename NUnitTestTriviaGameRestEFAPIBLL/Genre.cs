using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TriviaGameRestEFAPI.BLL;
using TriviaGameRestEFAPI.BLL.DataContract;
using TriviaGameRestEFAPI.Data;

namespace NUnitTestTriviaGameRestEFAPIBLL
{
    public class Genre
    {
        [SetUp]
        public void Setup()
        {
            Utilities.DatabaseHelper.ConnectionString = TestHelper.DBConnection;
        }

        [Test]
        public async Task GetGenres()
        {
            List<GenreResp> _genreResps = await TriviaGameRestEFAPIBLL.GenreHelper.GetGenres();

            Assert.IsTrue(_genreResps.Count == 3);
        }

        [Test]
        public async Task UpdateGenre()
        {
            await TriviaGameRestEFAPIBLL.GenreHelper.UpdateGenre(Guid.Parse("51983145-3206-4708-afef-4a345e5c8d65"), "Comedy");

            Assert.IsTrue(true);
        }

        [Test]
        public async Task CreateGame()
        {
            GameResp _gameResp = await TriviaGameRestEFAPIBLL.GameHelper.CreateGame("Action");

            Assert.IsTrue(_gameResp.GenreName == "Action");
        }

        [Test]
        public async Task AnswerQuestion()
        {
            GameResp _gameResp = await TriviaGameRestEFAPIBLL.GameHelper.CreateGame("Horror");

            foreach (var question in _gameResp.Questions)
            {
                await TriviaGameRestEFAPIBLL.GameHelper.AnswerQuestion(new AnswerQuestionReq()
                {
                    GameId = _gameResp.GameId,
                    QuestionId = question.QuestionId,
                    ChoiceId = question.Choices.Where(choice => choice.IsCorrect).FirstOrDefault().ChoiceId,
                    AnswerDuration = 10
                });
            }

            Assert.IsTrue(true);
        }

        [Test]
        public async Task ViewGame()
        {
            GameResultResp _gameResultResp = await TriviaGameRestEFAPIBLL.GameHelper.ViewGame(Guid.Parse("2205001c-6d1c-4bcb-9896-68ef66dbfc25"));

            Assert.IsTrue(_gameResultResp.GameId == Guid.Parse("2205001c-6d1c-4bcb-9896-68ef66dbfc25"));
        }
    }
}