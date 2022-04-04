using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoCqrs.Domains.Commands;
using ToDoCqrs.Domains.Entities;
using ToDoCqrs.Domains.Repositories;
using ToDoCqrs.Notifications;

namespace ToDoCqrs.Domains.Handlers
{
    public class UserCommandHandler : IRequestHandler<UserCreateCommand, string>
                                    , IRequestHandler<UserRemoveCommand, string>
                                    , IRequestHandler<DoneCommand, string>
                                    , IRequestHandler<ToDoCommand, string>
    {

        private readonly UserRepository _repository;
        private readonly IMediator _mediator;

        public UserCommandHandler(IMediator mediator, UserRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByEmail(request.Email);
            if(user != null)
                throw new ArgumentException("E-mail indisponivel!");

            user = new UserEntity(request.Email);
            await _repository.Save(user);

            await _mediator.Publish(
                new UserActionNotification(user.Id, user.Email, ActionNotification.Created), 
                cancellationToken);
            
            return await Task.FromResult("User save successfully");
        }

        public async Task<string> Handle(UserRemoveCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Id);
            await _repository.Delete(user.Id);
            
            await _mediator.Publish(
                new UserActionNotification(user.Id, user.Email, ActionNotification.Deleted), 
                cancellationToken);

            return await Task.FromResult("Student deleted successfully");
        }

        public async Task<string> Handle(ToDoCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.UserId);
            var toDo = new ToDoEntity(request.Name, System.DateTime.Now);
            
            user.AddToDo(toDo);
            await _repository.Update(user.Id, user);

            await _mediator.Publish(
                new UserActionNotification(user.Id, user.Email, ActionNotification.Updated), 
                cancellationToken);
                
            await _mediator.Publish(
                new ToDoActionNotification(toDo.Id, toDo.Name, toDo.Status, ActionNotification.Created), 
                cancellationToken);

            return await Task.FromResult("To do save successfully");
        }

        public async Task<string> Handle(DoneCommand request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAll();
            var user = users.SelectMany(x => x.ToDoList, (x, t) => new { User = x, ToDo = t})
                            .Where(x => x.ToDo.Id.Equals(request.Id))
                            .Select(x => x.User)
                            .First();
            
            var toDo = user.Done(request.Id);
            await _repository.Update(user.Id, user);
            
            await _mediator.Publish(
                new UserActionNotification(user.Id, user.Email, ActionNotification.Updated), 
                cancellationToken);
                
            await _mediator.Publish(
                new ToDoActionNotification(toDo.Id, toDo.Name, toDo.Status, ActionNotification.Updated), 
                cancellationToken);

            return await Task.FromResult("To do done successfully");

        }

    }
}