using System.Threading.Tasks;

namespace Voting.Domain.Infra.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        Task Commit();
        Task RollBack();
    }
}