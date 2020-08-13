using System.Threading.Tasks;
using Voting.Domain.Commands;
using Voting.Domain.Handlers;
using Voting.Domain.Infra.Repositories.Contracts;
using Voting.Domain.Tests.FakeRepositories;
using NUnit.Framework;

namespace Voting.Domain.Tests.Handlers
{
    public class CreateHungryProfessionalHandlerTests
    {
        private IHungryProfessionalRepository _hungryProfessionalRepository;
        private IUnitOfWork _unitOfWork;
        private AddHungryProfessionalCommand _addHungryProfessionalCommandInvalid;
        private AddHungryProfessionalCommand _addHungryProfessionalCommandValid;
        private CreateHungryProfessionalHandler _handler;
        private const string HungryProfessionalNameValid = "John";
        private const string HungryProfessionalPasswordValid = "123!@#";
        private const string HungryProfessionalNameAlreadyRegisteredValid = "João";
        private CommandResult _commandResult;

        [SetUp]
        public void Setup()
        {
            _hungryProfessionalRepository = new HungryProfessionalFakeRepository();
            _unitOfWork = new UnitOfWorkFake();

            _addHungryProfessionalCommandInvalid = new AddHungryProfessionalCommand();
            _addHungryProfessionalCommandValid =
                new AddHungryProfessionalCommand(HungryProfessionalNameValid, HungryProfessionalPasswordValid);

            _handler = new CreateHungryProfessionalHandler(_hungryProfessionalRepository, _unitOfWork);
        }

        [Test]
        [Category("Handlers/CreateFavoriteRestaurant")]
        public async Task DadoUmAddHungryProfessionalComComandoInvalidoOProfissionalNaoDeveSerCadastrado()
        {
            _commandResult = (CommandResult) await _handler.Handle(_addHungryProfessionalCommandInvalid);
            Assert.AreEqual(false, _commandResult.Sucess);
        }

        [Test]
        [Category("Handlers/CreateFavoriteRestaurant")]
        public async Task DadoUmAddHungryProfessionalValidoQueJaEstaCadastradoOProfissionalNaoDeveSerCadastrado()
        {
            _addHungryProfessionalCommandValid =
                new AddHungryProfessionalCommand(HungryProfessionalNameAlreadyRegisteredValid,
                    HungryProfessionalPasswordValid);
            _commandResult = (CommandResult) await _handler.Handle(_addHungryProfessionalCommandValid);
            Assert.AreEqual(false, _commandResult.Sucess);
        }

        [Test]
        [Category("Handlers/CreateFavoriteRestaurant")]
        public async Task DadoUmAddHungryProfessionalValidoOProfissionalDeveSerCadastrado()
        {
            _commandResult = (CommandResult) await _handler.Handle(_addHungryProfessionalCommandValid);
            Assert.AreEqual(true, _commandResult.Sucess);
        }
    }
}