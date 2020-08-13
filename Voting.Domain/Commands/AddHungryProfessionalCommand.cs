using Flunt.Notifications;
using Flunt.Validations;
using Voting.Domain.Commands.Contracts;

namespace Voting.Domain.Commands
{
    public class AddHungryProfessionalCommand : Notifiable, ICommand
    {
        public AddHungryProfessionalCommand() { }
        
        public AddHungryProfessionalCommand(string hungryProfessionalName, string hungryProfessionalPassword)
        {
            HungryProfessionalName = hungryProfessionalName;
            HungryProfessionalPassword = hungryProfessionalPassword;
        }

        public string HungryProfessionalName { get; set; }
        public string HungryProfessionalPassword { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(HungryProfessionalName, 2, "HungryProfessionalName",
                        "Nome do profissional deve conter pelo menos 2 caracteres.")
                    .HasMinLen(HungryProfessionalPassword, 6, "HungryProfessionalPassword",
                        "Senha do profissional deve conter pelo menos 6 caracteres."));
        }
    }
}