using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Infra.Data;
using Voting.Domain.Infra.Repositories.Contracts;

namespace Voting.Domain.Infra.Repositories
{
    public class FavoriteRestaurantRepository : IFavoriteRestaurantRepository
    {
        private readonly DataContextInMemory _dataContextInMemory;

        public FavoriteRestaurantRepository(DataContextInMemory dataContextInMemory) =>
            _dataContextInMemory = dataContextInMemory;

        public async Task AddFavoriteRestaurant(FavoriteRestaurant favoriteRestaurant) =>
            await _dataContextInMemory.AddFavoriteRestaurant(favoriteRestaurant);

        public async Task<bool> IsTheFavoriteRestaurantRegistered(string favoriteRestauranteName) =>
            _dataContextInMemory.FavoriteRestaurants
                .FirstOrDefault(favoriteRestaurant =>
                    favoriteRestaurant.Name == favoriteRestauranteName) != null;
        
        public async Task<FavoriteRestaurant> GetFavoriteRestaurant(string favoriteRestaurantCode) =>
            await Task.FromResult(_dataContextInMemory.FavoriteRestaurants
                .FirstOrDefault(favoriteRestaurant => favoriteRestaurant.Code.Number == favoriteRestaurantCode));

        public async Task<IEnumerable<FavoriteRestaurant>> GetFavoriteRestaurants() =>
            await Task.FromResult(_dataContextInMemory.FavoriteRestaurants);
    }
}