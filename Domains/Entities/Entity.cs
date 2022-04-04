using System;

namespace ToDoCqrs.Domains.Entities
{
    public class Entity
    {
        public string Id { get; private set; }

        public Entity()
        {
            this.Id = Guid.NewGuid()
                          .ToString("N")
                          .ToLower();
        }
    }
}