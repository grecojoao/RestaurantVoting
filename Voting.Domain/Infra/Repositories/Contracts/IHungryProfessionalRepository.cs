using System.Threading.Tasks;
using Voting.Domain.Entities;

namespace Voting.Domain.Infra.Repositories.Contracts
{
    public interface IHungryProfessionalRepository
    {
        Task AddHungryProfessional(HungryProfessional hungryProfessional);
        Task<HungryProfessional> Get(string hungryProfessionalCode);
        Task<bool> IsTheHungryProfessionalRegistered(string hungryProfessionalName);
    }
}