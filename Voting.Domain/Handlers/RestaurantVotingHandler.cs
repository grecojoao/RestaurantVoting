using System;
using System.Threading.Tasks;
using Voting.Domain.Commands;
using Voting.Domain.Commands.Contracts;
using Voting.Domain.Handlers.Contracts;
using Voting.Domain.Infra.Repositories.Contracts;
using Voting.Domain.Services.Contracts;
using Flunt.Notifications;

namespace Voting.Domain.Handlers
{
    public class RestaurantVotingHandler : Notifiable,
        IHandler<VoteInMyFavoriteRestaurantCommand>
    {
        private readonly IRestaurantVotingService _restaurantVoting;
        private readonly IHungryProfessionalRepository _hungryProfessionalRepository;
        private readonly IFavoriteRestaurantRepository _favoriteRestaurantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantVotingHandler(IRestaurantVotingService restaurantVoting,
            IHungryProfessionalRepository hungryProfessionalRepository,
            IFavoriteRestaurantRepository favoriteRestaurantRepository,
            IUnitOfWork unitOfWork)
        {
            _restaurantVoting = restaurantVoting;
            _hungryProfessionalRepository = hungryProfessionalRepository;
            _favoriteRestaurantRepository = favoriteRestaurantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(VoteInMyFavoriteRestaurantCommand voteInMyFavoriteRestaurantCommand)
        {
            voteInMyFavoriteRestaurantCommand.Validate();
            if (voteInMyFavoriteRestaurantCommand.Invalid)
                return new CommandResult(false, "Voto Inválido.", voteInMyFavoriteRestaurantCommand.Notifications);

            if (!await _restaurantVoting.IsHappening())
                return new CommandResult(false, "Votação Encerrada.", null);

            var hungryProfessional =
                await _hungryProfessionalRepository.Get(voteInMyFavoriteRestaurantCommand.HungryProfessionalCode);
            if (hungryProfessional != null)
                if (await _restaurantVoting.ChecksIfTheProfessionalHasAlreadyVoted(hungryProfessional.Code.Number))
                    AddNotification(new Notification("HungryProfessional", "Você já votou em um Restaurante hoje!"));
            if (hungryProfessional == null)
                AddNotification(new Notification("HungryProfessionalCode", "Profissional faminto não cadastrado."));

            var favoriteRestaurant =
                await _favoriteRestaurantRepository.GetFavoriteRestaurant(
                    voteInMyFavoriteRestaurantCommand.FavoriteRestaurantCode);
            if (favoriteRestaurant == null)
                AddNotification(new Notification("FavoriteRestaurantCode", "Restaurante favorito não cadastrado."));
            if (favoriteRestaurant != null)
                if (!await _restaurantVoting.CanTheRestaurantBeVoted(favoriteRestaurant?.Code))
                    AddNotification(new Notification("FavoriteRestaurantCode",
                        "Você não pode votar em um Restaurante que já foi escolhido essa semana."));

            if (Invalid)
                return new CommandResult(false, "Voto Inválido.", Notifications);

            try
            {
                await _restaurantVoting.Vote(hungryProfessional?.Code, favoriteRestaurant?.Code);
                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBack();
                return new CommandResult(false, "Problemas no banco de dados.", ex);
            }

            return new CommandResult(true, "Votou!!",
                $"Profissional faminto: {hungryProfessional?.Code?.Number}, Restaurante favorito: {favoriteRestaurant?.Code?.Number}");
        }
    }
}