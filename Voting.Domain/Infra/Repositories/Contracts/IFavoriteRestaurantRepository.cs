using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Domain.Entities;

namespace Voting.Domain.Infra.Repositories.Contracts
{
    public interface IFavoriteRestaurantRepository
    {
        Task AddFavoriteRestaurant(FavoriteRestaurant favoriteRestaurant);
        Task<bool> IsTheFavoriteRestaurantRegistered(string favoriteRestauranteName);
        Task<FavoriteRestaurant> GetFavoriteRestaurant(string favoriteRestaurantCode);
        Task<IEnumerable<FavoriteRestaurant>> GetFavoriteRestaurants();
    }
}