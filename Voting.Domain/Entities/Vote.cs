using System;
using Voting.Domain.Entities.ValueObjects;

namespace Voting.Domain.Entities
{
    public class Vote : Entity
    {
        public Vote(Code hungryProfessionalCode, Code favoriteRestaurantCode, Guid idRestaurantVoting)
        {
            FavoriteRestaurantCode = favoriteRestaurantCode;
            HungryProfessionalCode = hungryProfessionalCode;
            IdRestaurantVoting = idRestaurantVoting;
            Date = DateTime.Now;
        }

        public Code FavoriteRestaurantCode { get; private set; }
        public Code HungryProfessionalCode { get; private set; }
        public Guid IdRestaurantVoting { get; private set; }
        public DateTime Date { get; private set; }

        public override string ToString()
        {
            return $"Votação: {IdRestaurantVoting}, Profissional faminto: {HungryProfessionalCode.Number}, Restaurante favorito: {FavoriteRestaurantCode.Number}";
        }
    }
}