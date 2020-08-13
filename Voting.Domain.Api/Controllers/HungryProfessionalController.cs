using System.Threading.Tasks;
using Voting.Domain.Commands;
using Voting.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Voting.Domain.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class HungryProfessionalController : ControllerBase
    {
        [Route("AddHungryProfessional")]
        [HttpPost]
        public async Task<CommandResult> AddHungryProfessional(
            [FromBody] AddHungryProfessionalCommand command,
            [FromServices] CreateHungryProfessionalHandler handler)
        {
            return (CommandResult) await handler.Handle(command);
        }
    }
}