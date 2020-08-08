using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;

namespace Voting.Domain.Services.Contracts
{
    public interface IRestaurantVotingService
    {
        Task<IList<FavoriteRestaurant>> GetCompetitors();
        Task<bool> IsHappening();
        Task<bool> CanTheRestaurantBeVoted(Code favoriteRestaurantCode);
        Task Vote(Code hungryProfessionalCode, Code favoriteRestaurantCode);
        Task<bool> ChecksIfTheProfessionalHasAlreadyVoted(string hungryProfessionalCode);
        Task<WinnerRestaurant> GetWinnerOfDay();
    }
}