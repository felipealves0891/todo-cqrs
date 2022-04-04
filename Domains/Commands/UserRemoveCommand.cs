using MediatR;

namespace ToDoCqrs.Domains.Commands
{
    public class UserRemoveCommand : IRequest<string> 
    {
        public string Id { get; private set; }
        
        public UserRemoveCommand(string id)
        {
            Id = id;
        }

    }
}