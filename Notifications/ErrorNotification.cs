using MediatR;

namespace ToDoCqrs.Notifications
{
    public class ErrorNotification : INotification
    {
        public ErrorNotification(string message, string stackTrace)
        {
            Message = message;
            StackTrace = stackTrace;
        }

        public string Message { get; private set; }

        public string StackTrace { get; private set; }
    }
}