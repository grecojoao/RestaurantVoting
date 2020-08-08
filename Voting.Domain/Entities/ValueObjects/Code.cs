namespace Voting.Domain.Entities.ValueObjects
{
    public class Code
    {
        public Code() { }
        public Code(string number)
        {
            Number = number;
        }

        public string Number { get; private set; }
    }
}