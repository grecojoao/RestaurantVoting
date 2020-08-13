using Voting.Domain.Entities.ValueObjects;

namespace Voting.Domain.Entities
{
    public class FavoriteRestaurant : Entity
    {
        public FavoriteRestaurant(Code code, string name)
        {
            Code = code;
            Name = name;
        }

        public Code Code { get; private set; }
        public string Name { get; private set; }

        public override string ToString() =>
            $"Restaurante: {Name}, Código: {Code.Number}";
    }
}