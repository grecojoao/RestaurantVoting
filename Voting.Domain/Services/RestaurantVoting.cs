using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;
using Voting.Domain.Infra.Repositories.Contracts;
using Voting.Domain.Services.Contracts;

namespace Voting.Domain.Services
{
    public class RestaurantVoting : Entity, IRestaurantVotingService
    {
        private readonly IFavoriteRestaurantRepository _favoriteRestaurantRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly IWinnerRestaurantRepository _winnerRestaurantRepository;

        public RestaurantVoting(IFavoriteRestaurantRepository favoriteRestaurantRepository,
            IVoteRepository voteRepository,
            IWinnerRestaurantRepository winnerRestaurantRepository)
        {
            StartTime = new Time(07, 00);
            EndTime = new Time(11, 30);
            DurationInDays = 5;
            DayInNumber = 1;
            IsClosed = false;

            _favoriteRestaurantRepository = favoriteRestaurantRepository;
            _voteRepository = voteRepository;
            _winnerRestaurantRepository = winnerRestaurantRepository;
        }

        public RestaurantVoting(Time startTime, Time endTime, int durationInDays,
            IFavoriteRestaurantRepository favoriteRestaurantRepository,
            IVoteRepository voteRepository,
            IWinnerRestaurantRepository winnerRestaurantRepository)
        {
            StartTime = startTime;
            EndTime = endTime;
            DurationInDays = durationInDays;
            DayInNumber = 1;

            _favoriteRestaurantRepository = favoriteRestaurantRepository;
            _voteRepository = voteRepository;
            _winnerRestaurantRepository = winnerRestaurantRepository;
        }

        private Time StartTime { get; }
        private Time EndTime { get; }
        private int DurationInDays { get; }
        private int DayInNumber { get; set; }
        private bool IsClosed { get; set; }

        public async Task<IList<FavoriteRestaurant>> GetCompetitors()
        {
            var winners = _winnerRestaurantRepository.GetWinners(Id).Result.ToList();
            var restaurants = _favoriteRestaurantRepository.GetFavoriteRestaurants().Result.ToList();
            var favoriteRestaurants = restaurants;

            foreach (var restaurant in from restaurant in restaurants
                from winner in winners
                where restaurant.Code.Number == winner.FavoriteRestaurant.Code.Number
                select restaurant)
                favoriteRestaurants.Remove(restaurant);

            return favoriteRestaurants;
        }

        public async Task<bool> IsHappening()
        {
            var isHappening = TimeNowWithInTheVotingTime(DateTime.Now) && IsElectionDay();

            if (isHappening && IsClosed)
                IsClosed = false;

            if (!isHappening && !IsClosed)
                await End();
            
            return isHappening;
        }

        private bool TimeNowWithInTheVotingTime(DateTime dateNow) =>
            TimeIsAfterToStartTime(dateNow) && TimeIsBeforeToEndTime(dateNow);

        private bool TimeIsAfterToStartTime(DateTime dateNow) =>
            dateNow.Hour >= StartTime.Hour && dateNow.Minute >= StartTime.Minute;

        private bool TimeIsBeforeToEndTime(DateTime dateNow) =>
            dateNow.Hour <= EndTime.Hour && dateNow.Minute <= EndTime.Minute;

        private bool IsElectionDay() =>
            DayInNumber <= DurationInDays;

        public async Task<bool> CanTheRestaurantBeVoted(Code favoriteRestaurantCode) =>
            await _winnerRestaurantRepository.GetWinner(favoriteRestaurantCode.Number, Id) == null;

        public async Task Vote(Code hungryProfessionalCode, Code favoriteRestaurantCode)
        {
            var vote = new Vote(hungryProfessionalCode, favoriteRestaurantCode, Id);
            if (await IsHappening())
                await Task.FromResult(_voteRepository.AddVote(vote));
        }

        public async Task<bool> ChecksIfTheProfessionalHasAlreadyVoted(string hungryProfessionalCode) =>
            await _voteRepository.HungryProfessionalAlreadyVoted(hungryProfessionalCode, Id);

        private async Task End()
        {
            if (TimeNowIsAfterToEndTime(DateTime.Now) && IsElectionDay())
            {
                var winners = await _voteRepository.GetCompetitorWithMostVotes();
                if (winners.Count == 1)
                    await MarkAsWinner(winners.FirstOrDefault());
                else if (winners.Count > 1)
                    await MarkAsWinner(DrawWinner(winners));
                DayInNumber++;
                IsClosed = true;
            }
        }

        private bool TimeNowIsAfterToEndTime(DateTime dateNow) =>
            dateNow.Hour >= EndTime.Hour && dateNow.Minute >= EndTime.Minute;

        private async Task MarkAsWinner(Code favoriteRestaurantCode)
        {
            var favoriteRestaurant = await _favoriteRestaurantRepository.GetFavoriteRestaurant(favoriteRestaurantCode.Number);
            var winner = new WinnerRestaurant(favoriteRestaurant, Id);
            await _winnerRestaurantRepository.AddWinner(winner);
        }

        private static Code DrawWinner(IList<Code> restaurants)
        {
            var sorter = new Random();
            var winnerIndexOf = sorter.Next(0, restaurants.Count);
            return restaurants[winnerIndexOf];
        }

        public async Task<WinnerRestaurant> GetWinnerOfDay()
        {
            await IsHappening();
            return await _winnerRestaurantRepository.GetWinnerOfTheDay(Id);
        }

        public override string ToString()
        {
            return $"Votação: {Id}, Hora de início: {StartTime}, Hora de encerramento: {EndTime}";
        }
    }
}