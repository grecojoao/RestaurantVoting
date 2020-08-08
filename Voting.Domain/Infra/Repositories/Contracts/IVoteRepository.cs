using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;

namespace Voting.Domain.Infra.Repositories.Contracts
{
    public interface IVoteRepository
    {
        Task AddVote(Vote vote);
        Task<IList<Code>> GetCompetitorWithMostVotes();
        Task<bool> HungryProfessionalAlreadyVoted(string hungryProfessionalCode, Guid idRestaurantVoting);
    }
}