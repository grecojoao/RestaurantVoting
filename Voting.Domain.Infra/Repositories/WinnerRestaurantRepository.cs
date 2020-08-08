using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Infra.Data;
using Voting.Domain.Infra.Repositories.Contracts;
using Voting.Domain.Queries;

namespace Voting.Domain.Infra.Repositories
{
    public class WinnerRestaurantRepository : IWinnerRestaurantRepository
    {
        private readonly DataContextInMemory _dataContext;

        public WinnerRestaurantRepository(DataContextInMemory dataContextInMemory)
        {
            _dataContext = dataContextInMemory;
        }

        public async Task AddWinner(WinnerRestaurant winnerRestaurant) =>
            await Task.FromResult(_dataContext.AddWinner(winnerRestaurant));

        public async Task<WinnerRestaurant> GetWinnerOfTheDay(Guid idRestaurantVoting) =>
            await Task.FromResult(_dataContext.WinnerRestaurants
                .FirstOrDefault(WinnerRestaurantQueries.TodaysWinner(idRestaurantVoting)));

        public async Task<WinnerRestaurant> GetWinner(string winnerRestaurantCode, Guid idRestaurantVoting)=>
            await Task.FromResult(_dataContext.WinnerRestaurants
                .FirstOrDefault(WinnerRestaurantQueries.Winner(winnerRestaurantCode, idRestaurantVoting)));

        public async Task<IEnumerable<WinnerRestaurant>> GetWinners(Guid idRestaurantVoting) =>
            await Task.FromResult(_dataContext.WinnerRestaurants
                .Where(WinnerRestaurantQueries.Winners(idRestaurantVoting)));
    }
}