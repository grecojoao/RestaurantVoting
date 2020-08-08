using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Domain.Entities;

namespace Voting.Domain.Infra.Repositories.Contracts
{
    public interface IWinnerRestaurantRepository
    {
        Task AddWinner(WinnerRestaurant winnerRestaurant);
        Task<WinnerRestaurant> GetWinnerOfTheDay(Guid idRestaurantVoting);
        Task<WinnerRestaurant> GetWinner(string winnerRestaurantCode, Guid idRestaurantVoting);
        Task<IEnumerable<WinnerRestaurant>> GetWinners(Guid idRestaurantVoting);
    }
}