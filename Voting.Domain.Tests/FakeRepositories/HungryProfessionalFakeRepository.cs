using System.Threading.Tasks;
using Voting.Domain.Entities;
using Voting.Domain.Entities.ValueObjects;
using Voting.Domain.Infra.Repositories.Contracts;

namespace Voting.Domain.Tests.FakeRepositories
{
    public class HungryProfessionalFakeRepository : IHungryProfessionalRepository
    {
        public async Task AddHungryProfessional(HungryProfessional hungryProfessional) =>
            await Task.FromResult(true);

        public Task<HungryProfessional> Get(string hungryProfessionalCode)
            => hungryProfessionalCode == "112277" || hungryProfessionalCode == "999999"
                ? Task.FromResult(new HungryProfessional(new Code(hungryProfessionalCode), "João", "123!@#"))
                : Task.FromResult((HungryProfessional) null);

        public async Task<bool> IsTheHungryProfessionalRegistered(string hungryProfessionalName) =>
            await Task.FromResult(hungryProfessionalName == "João");
    }
}