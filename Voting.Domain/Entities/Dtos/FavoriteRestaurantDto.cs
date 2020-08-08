namespace Voting.Domain.Entities.Dtos
{
    public class FavoriteRestaurantDto
    {
        public FavoriteRestaurantDto() { }

        public FavoriteRestaurantDto(string codigo, string nome)
        {
            Nome = nome;
            Codigo = codigo;
        }
        
        public string Nome { get;  set; }
        public string Codigo { get;  set; }
    }
}