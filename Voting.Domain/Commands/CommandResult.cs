using Voting.Domain.Commands.Contracts;

namespace Voting.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() { }
        public CommandResult(bool sucess, string message, object data)
        {
            Sucess = sucess;
            Message = message;
            Data = data;
        }

        public bool Sucess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}