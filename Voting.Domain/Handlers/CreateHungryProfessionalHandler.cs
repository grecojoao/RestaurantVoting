using System;
using System.Threading.Tasks;
using Flunt.Notifications;
using Voting.Domain.Commands;
using Voting.Domain.Commands.Contracts;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;
using Voting.Domain.Handlers.Contracts;
using Voting.Domain.Infra.Repositories.Contracts;

namespace Voting.Domain.Handlers
{
    public class CreateHungryProfessionalHandler : Notifiable,
        IHandler<AddHungryProfessionalCommand>
    {
        public CreateHungryProfessionalHandler(IHungryProfessionalRepository hungryProfessionalRepository, IUnitOfWork unitOfWork)
        {
            _hungryProfessionalRepository = hungryProfessionalRepository;
            _unitOfWork = unitOfWork;
        }

        private readonly IHungryProfessionalRepository _hungryProfessionalRepository;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<ICommandResult> Handle(AddHungryProfessionalCommand addHungryProfessionalCommand)
        {
            addHungryProfessionalCommand.Validate();
            if (addHungryProfessionalCommand.Invalid)
                return new CommandResult(false, "Profissional faminto Inválido",
                    addHungryProfessionalCommand.Notifications);
            
            if (await _hungryProfessionalRepository.IsTheHungryProfessionalRegistered(addHungryProfessionalCommand
                .HungryProfessionalName))
                return new CommandResult(false, "Profissional faminto Inválido",
                    new Notification("HungryProfessionalName", "O Profissional já está cadastrado."));

            var hungryProfessionalCode = new Code(Guid.NewGuid().ToString().Substring(0, 6));
            var hungryProfessional = new HungryProfessional(hungryProfessionalCode,
                addHungryProfessionalCommand.HungryProfessionalName, addHungryProfessionalCommand.HungryProfessionalPassword);

            try
            {
                await _hungryProfessionalRepository.AddHungryProfessional(hungryProfessional);
                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBack();
                return new CommandResult(false, "Problemas no banco de dados.", ex);
            }

            return new CommandResult(true, "Profissional faminto cadastrado.", hungryProfessional.ToString());
        }
    }
}