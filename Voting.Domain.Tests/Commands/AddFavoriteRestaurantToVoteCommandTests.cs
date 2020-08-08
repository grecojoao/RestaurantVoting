using Voting.Domain.Commands;
using NUnit.Framework;

namespace Voting.Domain.Tests.Commands
{
    public class AddFavoriteRestaurantToVoteCommandTests
    {
        private AddFavoriteRestaurantCommand _addFavoriteRestaurantToVoteCommandInvalid;
        private AddFavoriteRestaurantCommand _addFavoriteRestaurantToVoteCommandValid;

        [SetUp]
        public void Setup()
        {
            _addFavoriteRestaurantToVoteCommandInvalid = new AddFavoriteRestaurantCommand();

            const string favoriteRestauranteName = "Bistrô";
            _addFavoriteRestaurantToVoteCommandValid = new AddFavoriteRestaurantCommand(favoriteRestauranteName);
        }

        [Test]
        [Category("Commands")]
        public void DadoUmComandoInvalidoORetornoDeveSerInvalido()
        {
            _addFavoriteRestaurantToVoteCommandInvalid.Validate();
            Assert.False(_addFavoriteRestaurantToVoteCommandInvalid.Valid);
        }

        [Test]
        [Category("Commands")]
        public void DadoUmComandoValidoORetornoDeveSerValido()
        {
            _addFavoriteRestaurantToVoteCommandValid.Validate();
            Assert.True(_addFavoriteRestaurantToVoteCommandValid.Valid);
        }
    }
}