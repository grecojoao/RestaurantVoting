using System;
using System.Threading.Tasks;
using Voting.Domain.Commands;
using Voting.Domain.Commands.Contracts;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;
using Voting.Domain.Handlers.Contracts;
using Voting.Domain.Infra.Repositories.Contracts;
using Flunt.Notifications;

namespace Voting.Domain.Handlers
{
    public class CreateFavoriteRestaurantHandler : Notifiable,
        IHandler<AddFavoriteRestaurantCommand>
    {
        private readonly IFavoriteRestaurantRepository _favoriteRestaurantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFavoriteRestaurantHandler(IFavoriteRestaurantRepository favoriteRestaurantRepository,
            IUnitOfWork unitOfWork)
        {
            _favoriteRestaurantRepository = favoriteRestaurantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(AddFavoriteRestaurantCommand addFavoriteRestaurantCommand)
        {
            addFavoriteRestaurantCommand.Validate();
            if (addFavoriteRestaurantCommand.Invalid)
                return new CommandResult(false, "Restaurante favorito Inválido",
                    addFavoriteRestaurantCommand.Notifications);

            if (await _favoriteRestaurantRepository.IsTheFavoriteRestaurantRegistered(addFavoriteRestaurantCommand
                .FavoriteRestaurantName))
                return new CommandResult(false, "Restaurante favorito Inválido",
                    new Notification("FavoriteRestaurantName", "O Restaurante já está cadastrado."));

            var favoriteRestaurantCode = new Code(Guid.NewGuid().ToString().Substring(0, 6));
            var favoriteRestaurant = new FavoriteRestaurant(favoriteRestaurantCode,
                addFavoriteRestaurantCommand.FavoriteRestaurantName);

            try
            {
                await _favoriteRestaurantRepository.AddFavoriteRestaurant(favoriteRestaurant);
                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBack();
                return new CommandResult(false, "Problemas no banco de dados.", ex);
            }

            return new CommandResult(true, "Restaurante favorito cadastrado.", favoriteRestaurant.ToString());
        }
    }
}