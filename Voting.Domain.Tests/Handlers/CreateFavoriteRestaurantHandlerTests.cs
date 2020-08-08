using System.Threading.Tasks;
using Voting.Domain.Commands;
using Voting.Domain.Handlers;
using Voting.Domain.Infra.Repositories.Contracts;
using Voting.Domain.Tests.FakeRepositories;
using NUnit.Framework;

namespace Voting.Domain.Tests.Handlers
{
    public class CreateFavoriteRestaurantHandlerTests
    {
        private IFavoriteRestaurantRepository _favoriteRestaurantRepository;
        private IUnitOfWork _unitOfWork;
        private AddFavoriteRestaurantCommand _addFavoriteRestaurantCommandInvalid;
        private AddFavoriteRestaurantCommand _addFavoriteRestaurantCommandValid;
        private CreateFavoriteRestaurantHandler _handler;
        private const string FavoriteRestaurantNameValid = "Bistrô";
        private const string FavoriteRestaurantNameAlreadyRegisteredValid = "Trôbis";
        private CommandResult _commandResult;

        [SetUp]
        public void Setup()
        {
            _favoriteRestaurantRepository = new FavoriteRestaurantFakeRepository();
            _unitOfWork = new UnitOfWorkFake();

            _addFavoriteRestaurantCommandInvalid = new AddFavoriteRestaurantCommand();
            _addFavoriteRestaurantCommandValid = new AddFavoriteRestaurantCommand(FavoriteRestaurantNameValid);

            _handler = new CreateFavoriteRestaurantHandler(_favoriteRestaurantRepository, _unitOfWork);
        }

        [Test]
        [Category("Handlers/CreateFavoriteRestaurant")]
        public async Task DadoUmAddFavoriteRestaurantComComandoInvalidoORestauranteNaoDeveSerCadastrado()
        {
            _commandResult = (CommandResult) await _handler.Handle(_addFavoriteRestaurantCommandInvalid);
            Assert.AreEqual(false, _commandResult.Sucess);
        }

        [Test]
        [Category("Handlers/CreateFavoriteRestaurant")]
        public async Task DadoUmAddFavoriteRestaurantValidoQueJaEstaCadastradoORestauranteNaoDeveSerCadastrado()
        {
            _addFavoriteRestaurantCommandValid = new AddFavoriteRestaurantCommand(FavoriteRestaurantNameAlreadyRegisteredValid);
            _commandResult = (CommandResult) await _handler.Handle(_addFavoriteRestaurantCommandValid);
            Assert.AreEqual(false, _commandResult.Sucess);
        }

        [Test]
        [Category("Handlers/CreateFavoriteRestaurant")]
        public async Task DadoUmAddFavoriteRestaurantValidoORestauranteDeveSerCadastrado()
        {
            _commandResult = (CommandResult) await _handler.Handle(_addFavoriteRestaurantCommandValid);
            Assert.AreEqual(true, _commandResult.Sucess);
        }
    }
}