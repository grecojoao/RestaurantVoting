using System.Linq;
using System.Threading.Tasks;
using Voting.Domain.Commands;
using Voting.Domain.Entities.Dtos;
using Voting.Domain.Handlers;
using Voting.Domain.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Voting.Domain.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class RestaurantVotingController : ControllerBase
    {
        [Route("GetCompetitors")]
        [HttpGet]
        public async Task<CommandResult> GetCompetitors(
            [FromServices] IRestaurantVotingService restaurantVoting
        )
        {
            var favoriteRestaurants = await restaurantVoting.GetCompetitors();
            var favoriteRestaurantsDto = favoriteRestaurants.Select(favoriteRestaurant =>
                new FavoriteRestaurantDto(favoriteRestaurant.Code.Number, favoriteRestaurant.Name)).ToList();

            return new CommandResult(true, "Restaurantes favoritos que estão competindo:", favoriteRestaurantsDto);
        }

        [Route("SeeWhereWeEat")]
        [HttpGet]
        public async Task<CommandResult> SeeWhereWeEat(
            [FromServices] IRestaurantVotingService restaurantVoting
        )
        {
            var winner = await restaurantVoting.GetWinnerOfDay();
            return winner == null
                ? new CommandResult(false, "Votação em andamento!", "Ainda não sabemos onde vamos almoçar! =[")
                : new CommandResult(true, "Votação encerrada!", winner.ToString());
        }

        [Route("VoteInMyFavoriteRestaurant")]
        [HttpPost]
        public async Task<CommandResult> VoteInMyFavoriteRestaurant(
            [FromBody] VoteInMyFavoriteRestaurantCommand command,
            [FromServices] RestaurantVotingHandler handler)
        {
            return (CommandResult) await handler.Handle(command);
        }
    }
}