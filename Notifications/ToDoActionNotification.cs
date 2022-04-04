using System;
using MediatR;

namespace ToDoCqrs.Notifications
{
    public class ToDoActionNotification : INotification
    {
        public string Id { get; set; }

        public string Name { get; private set; }

        public bool Status { get; private set; }

        public ActionNotification Action { get; private set; }
        
        public ToDoActionNotification(
            string id, 
            string name,
            bool status,
            ActionNotification action
        ) {
            Id = id;
            Name = name;
            Status = status;
            Action = action;
        }

        
    }
}