using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoCqrs.Controllers.Dtos.Users;
using ToDoCqrs.Domains.Commands;
using ToDoCqrs.Domains.Repositories;
using ToDoCqrs.Services;

namespace ToDoCqrs.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserRepository _repository;
        private readonly TokenService _tokenGenerator;

        public UserController(
            IMediator mediator, 
            UserRepository repository,
            TokenService tokenGenerator)
        {
            _mediator = mediator;
            _repository = repository;
            _tokenGenerator = tokenGenerator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var user = await _repository.GetByEmail(dto.Email);
            if(user is null)
                return NotFound(new Payload(404, "NotFound", "E-mail invalido!"));

            var token = _tokenGenerator.GenerateToken(user);

            return Accepted(new Payload(202, "Accepted", new LoginResponseDto { Token = token}));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("create/{email}")]
        public async Task<IActionResult> Create(string email)
        {
            var command = new UserCreateCommand(email);
            var response = await _mediator.Send(command);
            return Created("", new Payload(201, "Created", response));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            string email = string.Empty;
            foreach(var claim in User.Claims)
                if(claim.Type == ClaimTypes.Email)
                    email = claim.Value;

            var entity = await _repository.GetByEmail(email);
            if(entity is null)
                return NotFound(new Payload(404, "NotFound", email));

            var response = await _mediator.Send(new UserRemoveCommand(entity.Id));
            return Ok(new Payload(200, "Ok", response));
        }
    }
}