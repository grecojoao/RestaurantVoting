using System.Threading.Tasks;
using Voting.Domain.Infra.Repositories.Contracts;

namespace Voting.Domain.Tests.FakeRepositories
{
    public class UnitOfWorkFake : IUnitOfWork
    {
        public async Task Commit() => await Task.FromResult(true);
        public async Task RollBack() => await Task.FromResult(true);
    }
}