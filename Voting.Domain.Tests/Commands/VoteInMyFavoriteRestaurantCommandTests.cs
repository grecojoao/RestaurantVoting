using Voting.Domain.Commands;
using NUnit.Framework;

namespace Voting.Domain.Tests.Commands
{
    public class VoteInMyFavoriteRestaurantCommandTests
    {
        private VoteInMyFavoriteRestaurantCommand _voteInMyFavoriteRestaurantCommandInvalid;
        private VoteInMyFavoriteRestaurantCommand _voteInMyFavoriteRestaurantCommandValid;

        [SetUp]
        public void Setup()
        {
            _voteInMyFavoriteRestaurantCommandInvalid = new VoteInMyFavoriteRestaurantCommand();

            const string hungryProfessionalCode = "014477";
            const string favoriteRestaurantCode = "4466";
            _voteInMyFavoriteRestaurantCommandValid = new VoteInMyFavoriteRestaurantCommand(hungryProfessionalCode, favoriteRestaurantCode);
        }

        [Test]
        [Category("Commands")]
        public void DadoUmComandoInvalidoORetornoDeveSerInvalido()
        {
            _voteInMyFavoriteRestaurantCommandInvalid.Validate();
            Assert.False(_voteInMyFavoriteRestaurantCommandInvalid.Valid);
        }

        [Test]
        [Category("Commands")]
        public void DadoUmComandoValidoORetornoDeveSerValido()
        {
            _voteInMyFavoriteRestaurantCommandValid.Validate();
            Assert.True(_voteInMyFavoriteRestaurantCommandValid.Valid);
        }
    }
}