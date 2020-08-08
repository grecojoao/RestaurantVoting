using System;
using System.Collections.Generic;
using System.Linq;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;
using Voting.Domain.Queries;
using NUnit.Framework;

namespace Voting.Domain.Tests.Queries
{
    public class WinnerRestaurantQueriesTests
    {
        private List<WinnerRestaurant> _winnerRestaurants;
        private Guid _idRestaurantVoting;
        private readonly Code _favoriteRestaurantCode01 = new Code("7777");
        private readonly Code _favoriteRestaurantCode02 = new Code("8888");

        [SetUp]
        public void Setup()
        {
            _idRestaurantVoting = Guid.NewGuid();
            _winnerRestaurants = new List<WinnerRestaurant>
            {
                new WinnerRestaurant(new FavoriteRestaurant(_favoriteRestaurantCode01, ""), _idRestaurantVoting),
                new WinnerRestaurant(new FavoriteRestaurant(_favoriteRestaurantCode02, ""), Guid.NewGuid())
            };
        }

        [Test]
        [Category("Queries")]
        public void
            DadoUmaQueryParaTrazerORestauranteVencedorDeHojeEmUmaRelacaoDeDoisVencedoresEmVotacoesDiferentesORetornoDeveSerDeUmRestaurante()
        {
            var winnerRestaurants = _winnerRestaurants
                .Where(WinnerRestaurantQueries.TodaysWinner(_idRestaurantVoting));
            Assert.AreEqual(1, winnerRestaurants.Count());
        }
    }
}