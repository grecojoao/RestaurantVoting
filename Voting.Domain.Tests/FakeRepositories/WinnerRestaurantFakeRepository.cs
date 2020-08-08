using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;
using Voting.Domain.Infra.Repositories.Contracts;

namespace Voting.Domain.Tests.FakeRepositories
{
    public class WinnerRestaurantFakeRepository : IWinnerRestaurantRepository
    {
        public Task AddWinner(WinnerRestaurant winnerRestaurant) =>
            Task.FromResult(true);

        public Task<WinnerRestaurant> GetWinnerOfTheDay(Guid election)
        {
            return Task.FromResult<WinnerRestaurant>(new WinnerRestaurant(new FavoriteRestaurant(new Code("1177"), ""), Guid.NewGuid()));
        }

        public Task<WinnerRestaurant> GetWinner(string winnerRestaurantCode, Guid idRestaurantVoting) =>
            winnerRestaurantCode == "7799"
                ? Task.FromResult(new WinnerRestaurant(new FavoriteRestaurant(new Code("7799"),"") , Guid.NewGuid()))
                : Task.FromResult((WinnerRestaurant) null);

        public Task<IEnumerable<WinnerRestaurant>> GetWinners(Guid idRestaurantVoting) =>
            null;
    }
}