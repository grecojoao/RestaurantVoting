using System.Linq;
using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Infra.Data;
using Voting.Domain.Infra.Repositories.Contracts;

namespace Voting.Domain.Infra.Repositories
{
    public class HungryProfessionalRepository : IHungryProfessionalRepository
    {
        private readonly DataContextInMemory _dataContext;

        public HungryProfessionalRepository(DataContextInMemory dataContextInMemory)
        {
            _dataContext = dataContextInMemory;
        }

        public async Task<HungryProfessional> Get(string hungryProfessionalCode)
            => await Task.FromResult(_dataContext.HungryProfessionals
                .FirstOrDefault(hungryProfessional => hungryProfessional.Code.Number == hungryProfessionalCode));
    }
}