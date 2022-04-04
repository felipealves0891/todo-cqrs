using System;
using MediatR;

namespace ToDoCqrs.Domains.Commands
{
    public class ToDoCommand : IRequest<string>
    {
        public string UserId { get; private set; }

        public string Name { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public ToDoCommand(string userId, string name)
        {
            UserId = userId;
            Name = name;
            CreatedAt = DateTime.Now;
        }

    }
}