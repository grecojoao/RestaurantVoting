using System;
using Voting.Domain.Entities;

namespace Voting.Domain.Queries
{
    public static class WinnerRestaurantQueries
    {
        public static Func<WinnerRestaurant, bool> TodaysWinner(Guid idRestaurantVoting) =>
            winnerRestaurant => winnerRestaurant.IdRestaurantVoting == idRestaurantVoting &&
                                winnerRestaurant.VictoryDate.Date == DateTime.Now.Date;
        
        public static Func<WinnerRestaurant, bool> Winner(string winnerRestaurantCode, Guid idRestaurantVoting) =>
            winnerRestaurant => winnerRestaurant.IdRestaurantVoting == idRestaurantVoting
                                && winnerRestaurant.FavoriteRestaurant.Code.Number == winnerRestaurantCode;

        public static Func<WinnerRestaurant, bool> Winners(Guid idRestaurantVoting) =>
            winnerRestaurant => winnerRestaurant.IdRestaurantVoting == idRestaurantVoting;
    }
}