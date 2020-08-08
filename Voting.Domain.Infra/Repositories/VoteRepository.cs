using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;
using Voting.Domain.Infra.Data;
using Voting.Domain.Infra.Repositories.Contracts;
using Voting.Domain.Queries;

namespace Voting.Domain.Infra.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly DataContextInMemory _dataContextInMemory;

        public VoteRepository(DataContextInMemory dataContextInMemory)
        {
            _dataContextInMemory = dataContextInMemory;
        }

        public async Task AddVote(Vote vote)
            => await _dataContextInMemory.AddVote(vote);

        public Task<IList<Code>> GetCompetitorWithMostVotes()
        {
            var votes = _dataContextInMemory.Votes
                .Where(VoteQueries.TodaysVotes())
                .GroupBy(x => x.FavoriteRestaurantCode.Number)
                .OrderByDescending(x => x.Count()).ToList();

            var winnerNumberOfVotes = votes.FirstOrDefault()?.Count();

            IList<Code> competitors = (
                    from voteGroupByQuantity in votes
                    where voteGroupByQuantity.Count() == winnerNumberOfVotes
                    select voteGroupByQuantity.ToArray()
                        .FirstOrDefault()?.FavoriteRestaurantCode)
                .ToList();

            return Task.FromResult(competitors);
        }

        public Task<bool> HungryProfessionalAlreadyVoted(string hungryProfessionalCode, Guid idRestaurantVoting) =>
            Task.FromResult(_dataContextInMemory
                                .Votes
                                .FirstOrDefault(
                                    VoteQueries.HungryProfessionalHasVotesInTheVote(
                                        hungryProfessionalCode, idRestaurantVoting))
                            != null);
    }
}