using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;

namespace Voting.Domain.Infra.Data
{
    public class DataContextInMemory
    {
        private List<HungryProfessional> _hungryProfessionalsTemp;
        private List<Vote> _votesTemp;
        private List<FavoriteRestaurant> _favoriteRestaurantsTemp;
        private List<WinnerRestaurant> _winnerRestaurantsTemp;

        private List<HungryProfessional> _hungryProfessionals;
        private List<Vote> _votes;
        private List<FavoriteRestaurant> _favoriteRestaurants;
        private List<WinnerRestaurant> _winnerRestaurants;

        public DataContextInMemory()
        {
            _hungryProfessionalsTemp = new List<HungryProfessional>();
            _votesTemp = new List<Vote>();
            _favoriteRestaurantsTemp = new List<FavoriteRestaurant>();
            _winnerRestaurantsTemp = new List<WinnerRestaurant>();

            _hungryProfessionals = new List<HungryProfessional>();
            _votes = new List<Vote>();
            _favoriteRestaurants = new List<FavoriteRestaurant>();
            _winnerRestaurants = new List<WinnerRestaurant>();

            InitialSettings();
        }

        private void InitialSettings()
        {
            _hungryProfessionalsTemp.Add(new HungryProfessional(new Code("123456"), name: "Mateus", password: "1234!@#$"));
            _hungryProfessionalsTemp.Add(new HungryProfessional(new Code("789123"), name: "Marcos", password: "1234!@#$"));
            _hungryProfessionalsTemp.Add(new HungryProfessional(new Code("456789"), name: "Lucas", password: "1234!@#$"));
            _hungryProfessionalsTemp.Add(new HungryProfessional(new Code("987654"), name: "João", password: "1234!@#$"));
            _hungryProfessionals.Add(new HungryProfessional(new Code("123456"), name: "Mateus", password: "1234!@#$"));
            _hungryProfessionals.Add(new HungryProfessional(new Code("789123"), name: "Marcos", password: "1234!@#$"));
            _hungryProfessionals.Add(new HungryProfessional(new Code("456789"), name: "Lucas", password: "1234!@#$"));
            _hungryProfessionals.Add(new HungryProfessional(new Code("987654"), name: "João", password: "1234!@#$"));

            _favoriteRestaurantsTemp.Add(new FavoriteRestaurant(new Code("8765"), "Bistrô Gastronomia"));
            _favoriteRestaurantsTemp.Add(new FavoriteRestaurant(new Code("9456"), "Trôbis Gastronomia"));
            _favoriteRestaurants.Add(new FavoriteRestaurant(new Code("8765"), "Bistrô Gastronomia"));
            _favoriteRestaurants.Add(new FavoriteRestaurant(new Code("9456"), "Trôbis Gastronomia"));
        }

        public ReadOnlyCollection<HungryProfessional> HungryProfessionals => _hungryProfessionals.AsReadOnly();

        public ReadOnlyCollection<Vote> Votes => _votes.AsReadOnly();

        public ReadOnlyCollection<FavoriteRestaurant> FavoriteRestaurants => _favoriteRestaurants.AsReadOnly();

        public ReadOnlyCollection<WinnerRestaurant> WinnerRestaurants => _winnerRestaurants.AsReadOnly();

        public async Task SaveChanges()
        {
            _hungryProfessionals = _hungryProfessionalsTemp;
            _votes = _votesTemp;
            _favoriteRestaurants = _favoriteRestaurantsTemp;
            _winnerRestaurants = _winnerRestaurantsTemp;
        }

        public async Task Discard()
        {
            _hungryProfessionalsTemp = _hungryProfessionals;
            _votesTemp = _votes;
            _favoriteRestaurantsTemp = _favoriteRestaurants;
            _winnerRestaurantsTemp = _winnerRestaurants;
        }

        public async Task AddVote(Vote vote) => _votesTemp.Add(vote);

        public async Task AddHungryProfessional(HungryProfessional hungryProfessional) =>
            _hungryProfessionalsTemp.Add(hungryProfessional);

        public async Task AddFavoriteRestaurant(FavoriteRestaurant restaurant) =>
            _favoriteRestaurantsTemp.Add(restaurant);

        public async Task AddWinner(WinnerRestaurant result) => _winnerRestaurantsTemp.Add(result);
    }
}