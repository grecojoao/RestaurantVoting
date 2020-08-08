using System.Threading.Tasks;
using Voting.Domain.Infra.Data;
using Voting.Domain.Infra.Repositories.Contracts;

namespace Voting.Domain.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContextInMemory _dataContext;

        public UnitOfWork(DataContextInMemory dataContextInMemory) => _dataContext = dataContextInMemory;

        public async Task Commit() =>
            await _dataContext.SaveChanges();

        public async Task RollBack() =>
            await _dataContext.Discard();
    }
}