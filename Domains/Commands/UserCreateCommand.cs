using MediatR;

namespace ToDoCqrs.Domains.Commands
{
    public class UserCreateCommand : IRequest<string>
    {
        public string Email { get; private set; }
        
        public UserCreateCommand(string email)
        {
            Email = email;
        }

    }
}