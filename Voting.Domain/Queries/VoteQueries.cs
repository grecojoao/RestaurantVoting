using System;
using Voting.Domain.Entities;

namespace Voting.Domain.Queries
{
    public static class VoteQueries
    {
        public static Func<Vote, bool> TodaysVotes() =>
            vote => vote.Date.Date == DateTime.Now.Date;

        public static Func<Vote, bool> HungryProfessionalHasVotesInTheVote(string hungryProfessionalCode, Guid idRestaurantVoting) =>
            vote => vote.Date.Date == DateTime.Now.Date &&
                    vote.HungryProfessionalCode.Number == hungryProfessionalCode &&
                    vote.IdRestaurantVoting == idRestaurantVoting;
    }
}