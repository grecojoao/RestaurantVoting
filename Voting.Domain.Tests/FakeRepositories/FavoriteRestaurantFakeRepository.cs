using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;
using Voting.Domain.Infra.Repositories.Contracts;

namespace Voting.Domain.Tests.FakeRepositories
{
    public class FavoriteRestaurantFakeRepository : IFavoriteRestaurantRepository
    {
        public async Task AddFavoriteRestaurant(FavoriteRestaurant favoriteRestaurant) =>
            await Task.FromResult(true);

        public Task<bool> IsTheFavoriteRestaurantRegistered(string favoriteRestauranteName) =>
            Task.FromResult(favoriteRestauranteName == "Trôbis");

        public Task<FavoriteRestaurant> GetFavoriteRestaurant(string favoriteRestaurantCode)
        {
            if (favoriteRestaurantCode == "1144")
                return Task.FromResult(new FavoriteRestaurant(new Code("1144"), ""));
            if (favoriteRestaurantCode == "7799")
                return Task.FromResult(new FavoriteRestaurant(new Code("7799"), ""));
            return Task.FromResult((FavoriteRestaurant) null);
        }

        public Task<IEnumerable<FavoriteRestaurant>> GetFavoriteRestaurants() =>
            null;
    }
}