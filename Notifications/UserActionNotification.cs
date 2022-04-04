using MediatR;

namespace ToDoCqrs.Notifications
{
    public class UserActionNotification : INotification
    {
        public string Id { get; private set; }

        public string Email { get; private set; }

        public ActionNotification Action { get; private set; }
        
        public UserActionNotification(string id, string email, ActionNotification action)
        {
            Id = id;
            Email = email;
            Action = action;
        }

    }
}