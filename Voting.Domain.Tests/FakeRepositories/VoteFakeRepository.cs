using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;
using Voting.Domain.Infra.Repositories.Contracts;

namespace Voting.Domain.Tests.FakeRepositories
{
    public class VoteFakeRepository : IVoteRepository
    {
        public async Task AddVote(Vote vote) =>
            await Task.FromResult(true);

        public async Task<IList<Code>> GetCompetitorWithMostVotes() =>
           await Task.FromResult(new List<Code> {new Code("1144")});

        public Task<bool> HungryProfessionalAlreadyVoted(string codeHungryProfessional, Guid election) =>
            codeHungryProfessional == "999999" ? Task.FromResult(true) : Task.FromResult(false);
    }
}