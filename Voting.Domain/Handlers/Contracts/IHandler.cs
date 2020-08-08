using System.Threading.Tasks;
using Voting.Domain.Commands.Contracts;

namespace Voting.Domain.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T addFavoriteRestaurantCommand);
    }
}