using System;
using System.Collections.Generic;
using System.Linq;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;
using Voting.Domain.Queries;
using NUnit.Framework;

namespace Voting.Domain.Tests.Queries
{
    public class VoteQueriesTests
    {
        private List<Vote> _votes;
        private Guid _idRestaurantVoting;
        private readonly Code _hungryProfessionalCode01 = new Code("123456");
        private readonly Code _hungryProfessionalCode02 = new Code("654321");
        private readonly Code _favoriteRestaurantCode = new Code("7777");

        [SetUp]
        public void Setup()
        {
            _idRestaurantVoting = Guid.NewGuid();
            _votes = new List<Vote>
            {
                new Vote(_hungryProfessionalCode01, _favoriteRestaurantCode, _idRestaurantVoting),
                new Vote(_hungryProfessionalCode02, _favoriteRestaurantCode, _idRestaurantVoting)
            };
        }

        [Test]
        [Category("Queries")]
        public void DadoUmaQueryParaTrazerTodosOsVotosDeHojeORetornoDeveSerDeDoisVotos()
        {
            var votes = _votes.Where(VoteQueries.TodaysVotes());
            Assert.AreEqual(2, votes.Count());
        }

        [Test]
        [Category("Queries")]
        public void DadoUmaQueryParaVerificarSeOProfissionalQueNaoVotouJaVotouORetornoDeveSerFalse()
        {
            var hungryProfessionalAlreadyVoted = _votes
                .Where(VoteQueries.HungryProfessionalHasVotesInTheVote("", Guid.NewGuid()))
                .Any();
            Assert.IsFalse(hungryProfessionalAlreadyVoted);
        }

        [Test]
        [Category("Queries")]
        public void DadoUmaQueryParaVerificarSeOProfissionalJaVotouORetornoDeveSerTrue()
        {
            var hungryProfessionalAlreadyVoted = _votes
                .Where(VoteQueries.HungryProfessionalHasVotesInTheVote(_hungryProfessionalCode01.Number,
                    _idRestaurantVoting))
                .Any();
            Assert.True(hungryProfessionalAlreadyVoted);
        }
    }
}