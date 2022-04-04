using System;

namespace ToDoCqrs.Domains.Entities
{
    public class ToDoEntity : Entity
    {
        public string Name { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public Nullable<DateTime> ClosedAt { get; private set; }

        public bool Status => ClosedAt.HasValue;
        
        public ToDoEntity(string name, DateTime createdAt)
            : base()
        {
            this.Name = name;
            this.CreatedAt = createdAt;
        }

        public void Done()
        {
            ClosedAt = DateTime.Now;
        }
    }
}