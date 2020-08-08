using System.Threading.Tasks;
using Voting.Domain.Commands;
using Voting.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Voting.Domain.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class FavoriteRestaurantController : ControllerBase
    {
        [Route("AddFavoriteRestaurant")]
        [HttpPost]
        public async Task<CommandResult> AddFavoriteRestaurant(
            [FromBody] AddFavoriteRestaurantCommand command,
            [FromServices] CreateFavoriteRestaurantHandler handler)
        {
            return  (CommandResult) await handler.Handle(command);
        }
    }
}