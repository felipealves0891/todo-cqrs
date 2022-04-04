using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoCqrs.Notifications;

namespace ToDoCqrs.Events
{
    public class SuccessHandler 
        : INotificationHandler<UserActionNotification>
        , INotificationHandler<ToDoActionNotification>

    {
        public Task Handle(UserActionNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("The user {0} was {1} successfully", notification.Email, notification.Action.ToString().ToLower());
            });
        }

        public Task Handle(ToDoActionNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("The to do {0} was {1} successfully", notification.Name, notification.Action.ToString().ToLower());
            });
        }
    }
}