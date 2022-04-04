using System;
using MediatR;

namespace ToDoCqrs.Domains.Commands
{
    public class DoneCommand : IRequest<string>
    {
        public string Id { get; private set; }

        public DoneCommand(string id)
        {
            Id = id;
        }

    }
}