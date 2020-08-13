using System.Linq;
using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Infra.Data;
using Voting.Domain.Infra.Repositories.Contracts;

namespace Voting.Domain.Infra.Repositories
{
    public class HungryProfessionalRepository : IHungryProfessionalRepository
    {
        private readonly DataContextInMemory _dataContextInMemory;

        public HungryProfessionalRepository(DataContextInMemory dataContextInMemoryInMemory)
        {
            _dataContextInMemory = dataContextInMemoryInMemory;
        }

        public async Task AddHungryProfessional(HungryProfessional hungryProfessional) =>
            await _dataContextInMemory.AddHungryProfessional(hungryProfessional);

        public async Task<HungryProfessional> Get(string hungryProfessionalCode)
            => await Task.FromResult(_dataContextInMemory.HungryProfessionals
                .FirstOrDefault(hungryProfessional => hungryProfessional.Code.Number == hungryProfessionalCode));

        public async Task<bool> IsTheHungryProfessionalRegistered(string hungryProfessionalName) =>
            _dataContextInMemory.HungryProfessionals
                .FirstOrDefault(hungryProfessional =>
                    hungryProfessional.Name == hungryProfessionalName) != null;
    }
}