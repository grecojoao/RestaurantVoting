namespace Voting.Domain.Commands.Contracts
{
    public interface ICommand
    {
        void Validate();
    }
}