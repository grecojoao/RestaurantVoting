using Voting.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Voting.Domain.Commands
{
    public class AddFavoriteRestaurantCommand : Notifiable, ICommand
    {
        public AddFavoriteRestaurantCommand() { }

        public AddFavoriteRestaurantCommand(string favoriteRestaurantName)
        {
            FavoriteRestaurantName = favoriteRestaurantName;
        }

        public string FavoriteRestaurantName { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(FavoriteRestaurantName, 2, "FavoriteRestaurantName",
                        "Nome do restaurante deve conter pelo menos 2 caracteres."));
        }
    }
}