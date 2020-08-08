using System.Threading.Tasks;
using Voting.Domain.Entities;

namespace Voting.Domain.Infra.Repositories.Contracts
{
    public interface IHungryProfessionalRepository
    {
        Task<HungryProfessional> Get(string hungryProfessionalCode);
    }
}