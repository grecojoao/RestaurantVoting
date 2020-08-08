using Voting.Domain.Entities.ValueObjects;

namespace Voting.Domain.Entities
{
    public class HungryProfessional : Entity
    {
        public HungryProfessional(Code code, string name, string password)
        {
            Code = code;
            Name = name;
            Password = password;
        }

        public Code Code { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
    }
}