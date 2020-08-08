using Voting.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Voting.Domain.Commands
{
    public class VoteInMyFavoriteRestaurantCommand : Notifiable, ICommand
    {
        public VoteInMyFavoriteRestaurantCommand() { }

        public VoteInMyFavoriteRestaurantCommand(string hungryProfessionalCode, string favoriteRestaurantCode)
        {
            HungryProfessionalCode = hungryProfessionalCode;
            FavoriteRestaurantCode = favoriteRestaurantCode;
        }

        public string HungryProfessionalCode { get; set; }
        public string FavoriteRestaurantCode { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(HungryProfessionalCode, 6, "HungryProfessionalCode",
                        "Código deve conter pelo menos 6 caracteres.")
                    .HasMinLen(FavoriteRestaurantCode, 4, "FavoriteRestaurantCode",
                        "Código deve conter pelo menos 4 caracteres."));
        }
    }
}