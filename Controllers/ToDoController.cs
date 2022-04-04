using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoCqrs.Controllers.Dtos.ToDos;
using ToDoCqrs.Domains.Commands;
using ToDoCqrs.Domains.Repositories;

namespace ToDoCqrs.Controllers
{
    [Route("api/todo")]
    [ApiController]
    [Authorize]
    public class ToDoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserRepository _repository;

        public ToDoController(IMediator mediator, UserRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateToDoRequestDto dto)
        {
            string email = string.Empty;
            foreach(var claim in User.Claims)
                if(claim.Type == ClaimTypes.Email)
                    email = claim.Value;

            var user = await _repository.GetByEmail(email);
            ToDoCommand command = new ToDoCommand(user.Id, dto.Name);

            string response = await _mediator.Send(command);
            return Created("", new Payload(201, "Created", response));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var toDos = await _repository.GetAll();
            return Ok(new Payload(200, "Ok", toDos));
        }

        [HttpGet]
        [Route("done/{id}")]
        public async Task<IActionResult> DoneAsync(string id)
        {
            DoneCommand command = new DoneCommand(id);
            string response = await _mediator.Send(command);
            return Ok(new Payload(200, "Ok", response));
        }
    }
}