using System;

namespace Voting.Domain.Entities
{
    public class WinnerRestaurant : Entity
    {
        public WinnerRestaurant(FavoriteRestaurant favoriteRestaurant, Guid idRestaurantVoting)
        {
            FavoriteRestaurant = favoriteRestaurant;
            IdRestaurantVoting = idRestaurantVoting;
            VictoryDate = DateTime.Now;
        }

        public FavoriteRestaurant FavoriteRestaurant { get; private set; }
        public Guid IdRestaurantVoting { get; private set; }
        public DateTime VictoryDate { get; private set; }

        public override string ToString()
        {
            return $"Votação: {IdRestaurantVoting}, Data: {VictoryDate.Date}, {FavoriteRestaurant}";
        }
    }
}