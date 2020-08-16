using System;
using System.Threading.Tasks;
using Voting.Domain.Commands;
using Voting.Domain.Entities.ValueObjects;
using Voting.Domain.Handlers;
using Voting.Domain.Infra.Repositories.Contracts;
using Voting.Domain.Services;
using Voting.Domain.Tests.FakeRepositories;
using NUnit.Framework;

namespace Voting.Domain.Tests.Handlers
{
    public class RestaurantVotingHandlerTests
    {
        private IHungryProfessionalRepository _hungryProfessionalRepository;
        private IFavoriteRestaurantRepository _favoriteRestaurantRepository;
        private IVoteRepository _voteRepository;
        private IWinnerRestaurantRepository _winnerRestaurantRepository;
        private IUnitOfWork _unitOfWork;
        private RestaurantVoting _restaurantVoting;
        private RestaurantVotingHandler _handler;
        private VoteInMyFavoriteRestaurantCommand _voteInMyFavoriteRestaurantCommandValid;
        private VoteInMyFavoriteRestaurantCommand _voteInMyFavoriteRestaurantCommandInvalid;
        private CommandResult _commandResult;
        private const string HungryProfessionalCodeValid = "112277";
        private const string FavoriteRestaurantCodeValid = "1144";
        private string _hungryProfessionalCodeInvalid;
        private string _favoriteRestaurantCodeInvalid;
        private DateTime _dateNow;
        private Time _startTime;
        private Time _endTime;
        private const int DurationInDays = 1;

        [SetUp]
        public void Setup()
        {
            _hungryProfessionalRepository = new HungryProfessionalFakeRepository();
            _voteRepository = new VoteFakeRepository();
            _favoriteRestaurantRepository = new FavoriteRestaurantFakeRepository();
            _winnerRestaurantRepository = new WinnerRestaurantFakeRepository();
            _unitOfWork = new UnitOfWorkFake();

            _hungryProfessionalCodeInvalid = "";
            _favoriteRestaurantCodeInvalid = "";
            _voteInMyFavoriteRestaurantCommandInvalid =
                new VoteInMyFavoriteRestaurantCommand(_hungryProfessionalCodeInvalid, _favoriteRestaurantCodeInvalid);

            _voteInMyFavoriteRestaurantCommandValid =
                new VoteInMyFavoriteRestaurantCommand(HungryProfessionalCodeValid, FavoriteRestaurantCodeValid);

            _dateNow = DateTime.Now;
        }

        [Test]
        [Category("Handlers/VoteInMyFavoriteRestaurant")]
        public async Task DadoUmVotoComComandoInvalidoOVotoNaoDeveSerComputado()
        {
            _restaurantVoting = RestaurantVotingWithOneMinuteAvailable();
            _handler = HandlerConfigured();
            _commandResult = (CommandResult) await _handler.Handle(_voteInMyFavoriteRestaurantCommandInvalid);
            Assert.AreEqual(false, _commandResult.Sucess);
        }

        [Test]
        [Category("Handlers/VoteInMyFavoriteRestaurant")]
        public async Task DadoUmVotoForaDoPrazoDaVotacaoOVotoNaoDeveSerComputado()
        {
            _restaurantVoting = RestaurantVotingNoTimeAvailable();
            _handler = HandlerConfigured();
            _commandResult = (CommandResult) await _handler.Handle(_voteInMyFavoriteRestaurantCommandValid);
            Assert.AreEqual(false, _commandResult.Sucess);
        }

        [Test]
        [Category("Handlers/VoteInMyFavoriteRestaurant")]
        public async Task DadoUmVotoComProfissionalFamintoInvalidoOVotoNaoDeveSerComputado()
        {
            _restaurantVoting = RestaurantVotingWithOneMinuteAvailable();
            _handler = HandlerConfigured();
            _hungryProfessionalCodeInvalid = "000000";
            _voteInMyFavoriteRestaurantCommandValid =
                new VoteInMyFavoriteRestaurantCommand(_hungryProfessionalCodeInvalid, FavoriteRestaurantCodeValid);
            _commandResult = (CommandResult) await _handler.Handle(_voteInMyFavoriteRestaurantCommandValid);
            Assert.AreEqual(false, _commandResult.Sucess);
        }

        [Test]
        [Category("Handlers/VoteInMyFavoriteRestaurant")]
        public async Task DadoUmVotoDeUmProfissionalFamintoQueJaVotouOVotoNaoDeveSerComputado()
        {
            _restaurantVoting = RestaurantVotingWithOneMinuteAvailable();
            _handler = HandlerConfigured();
            _voteInMyFavoriteRestaurantCommandValid =
                new VoteInMyFavoriteRestaurantCommand("999999", FavoriteRestaurantCodeValid);
            _commandResult = (CommandResult) await _handler.Handle(_voteInMyFavoriteRestaurantCommandValid);

            Assert.AreEqual(false, _commandResult.Sucess);
        }

        [Test]
        [Category("Handlers/VoteInMyFavoriteRestaurant")]
        public async Task DadoUmVotoComRestauranteFavoritoNaoParticipanteOVotoNaoDeveSerComputado()
        {
            _restaurantVoting = RestaurantVotingWithOneMinuteAvailable();
            _handler = HandlerConfigured();
            _favoriteRestaurantCodeInvalid = "0000";
            _voteInMyFavoriteRestaurantCommandValid =
                new VoteInMyFavoriteRestaurantCommand(HungryProfessionalCodeValid, _favoriteRestaurantCodeInvalid);
            _commandResult = (CommandResult) await _handler.Handle(_voteInMyFavoriteRestaurantCommandValid);
            Assert.AreEqual(false, _commandResult.Sucess);
        }

        [Test]
        [Category("Handlers/VoteInMyFavoriteRestaurant")]
        public async Task DadoUmVotoComRestauranteFavoritoParticipanteQueJaFoiEscolhidoOVotoNaoDeveSerComputado()
        {
            var favoriteRestaurantCode = new Code("7799");
            _restaurantVoting = RestaurantVotingWithOneMinuteAvailable();
            _handler = HandlerConfigured();
            _voteInMyFavoriteRestaurantCommandValid =
                new VoteInMyFavoriteRestaurantCommand(HungryProfessionalCodeValid, favoriteRestaurantCode.Number);
            _commandResult = (CommandResult) await _handler.Handle(_voteInMyFavoriteRestaurantCommandValid);
            Assert.AreEqual(false, _commandResult.Sucess);
        }

        [Test]
        [Category("Handlers/VoteInMyFavoriteRestaurant")]
        public async Task DadoUmVotoValidoComHoraDeVotacaoMaiorQueAHoraInicialDaVotacaoOVotoDeveSerComputado()
        {
            _dateNow = _dateNow.AddHours(-1);
            _dateNow = _dateNow.AddMinutes(-1);
            _startTime = new Time(_dateNow.Hour, _dateNow.Minute);
            _dateNow = _dateNow.AddHours(1);
            _dateNow = _dateNow.AddMinutes(1);
            _endTime = new Time(_dateNow.Hour, _dateNow.Minute);

            _restaurantVoting = new RestaurantVoting(_startTime, _endTime, DurationInDays,
                _favoriteRestaurantRepository, _voteRepository, _winnerRestaurantRepository);

            _handler = HandlerConfigured();
            _commandResult = (CommandResult) await _handler.Handle(_voteInMyFavoriteRestaurantCommandValid);
            Assert.AreEqual(true, _commandResult.Sucess);
        }
        
        [Test]
        [Category("Handlers/VoteInMyFavoriteRestaurant")]
        public async Task DadoUmVotoValidoComHoraDeVotacaoIgualAHoraInicialDaVotacaoEMinutoDeVotacaoMaiorQueOMinutoInicialDaVotacaoOVotoDeveSerComputado()
        {
            _dateNow = _dateNow.AddMinutes(-1);
            _startTime = new Time(_dateNow.Hour, _dateNow.Minute);
            _dateNow = _dateNow.AddHours(1);
            _endTime = new Time(_dateNow.Hour, _dateNow.Minute);

            _restaurantVoting = new RestaurantVoting(_startTime, _endTime, DurationInDays,
                _favoriteRestaurantRepository, _voteRepository, _winnerRestaurantRepository);

            _handler = HandlerConfigured();
            _commandResult = (CommandResult) await _handler.Handle(_voteInMyFavoriteRestaurantCommandValid);
            Assert.AreEqual(true, _commandResult.Sucess);
        }
        
        [Test]
        [Category("Handlers/VoteInMyFavoriteRestaurant")]
        public async Task DadoUmVotoValidoComHoraDeVotacaoMenorQueAHoraFinalDaVotacaoOVotoDeveSerComputado()
        {
            _startTime = new Time(_dateNow.Hour, _dateNow.Minute);
            _dateNow = _dateNow.AddHours(1);
            _dateNow = _dateNow.AddMinutes(1);
            _endTime = new Time(_dateNow.Hour, _dateNow.Minute);

            _restaurantVoting = new RestaurantVoting(_startTime, _endTime, DurationInDays,
                _favoriteRestaurantRepository, _voteRepository, _winnerRestaurantRepository);

            _handler = HandlerConfigured();
            _commandResult = (CommandResult) await _handler.Handle(_voteInMyFavoriteRestaurantCommandValid);
            Assert.AreEqual(true, _commandResult.Sucess);
        }
        
        [Test]
        [Category("Handlers/VoteInMyFavoriteRestaurant")]
        public async Task DadoUmVotoValidoComHoraDeVotacaoIgualAHoraFinalDaVotacaoEMinutoDeVotacaoMenorQueOMinutoFinalDaVotacaoOVotoDeveSerComputado()
        {
            _dateNow = _dateNow.AddHours(-1);
            _dateNow = _dateNow.AddMinutes(1);
            _startTime = new Time(_dateNow.Hour, _dateNow.Minute);
            _dateNow = _dateNow.AddHours(1);
            _endTime = new Time(_dateNow.Hour, _dateNow.Minute);

            _restaurantVoting = new RestaurantVoting(_startTime, _endTime, DurationInDays,
                _favoriteRestaurantRepository, _voteRepository, _winnerRestaurantRepository);

            _handler = HandlerConfigured();
            _commandResult = (CommandResult) await _handler.Handle(_voteInMyFavoriteRestaurantCommandValid);
            Assert.AreEqual(true, _commandResult.Sucess);
        }
        
        private RestaurantVoting RestaurantVotingNoTimeAvailable()
        {
            _dateNow = _dateNow.AddMinutes(-2);
            _startTime = new Time(_dateNow.Hour, _dateNow.Minute);
            _dateNow = _dateNow.AddMinutes(1);
            _endTime = new Time(_dateNow.Hour, _dateNow.Minute);

            return new RestaurantVoting(_startTime, _endTime, DurationInDays,
                _favoriteRestaurantRepository, _voteRepository, _winnerRestaurantRepository);
        }

        private RestaurantVoting RestaurantVotingWithOneMinuteAvailable()
        {
            _startTime = new Time(_dateNow.Hour, _dateNow.Minute);
            _dateNow = _dateNow.AddMinutes(1);
            _endTime = new Time(_dateNow.Hour, _dateNow.Minute);

            return new RestaurantVoting(_startTime, _endTime, DurationInDays,
                _favoriteRestaurantRepository, _voteRepository, _winnerRestaurantRepository);
        }

        private RestaurantVotingHandler HandlerConfigured() =>
            new RestaurantVotingHandler(_restaurantVoting, _hungryProfessionalRepository, _favoriteRestaurantRepository,
                _unitOfWork);
    }
}