using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Authentication.Commands.Register;
using Netflix.Application.Authentication.Queries;
using Netflix.Contracts.Authentication.Common;
using Netflix.Contracts.Authentication.Login;
using Netflix.Contracts.Authentication.Register;

namespace Netflix.API.Controllers
{
    [ApiController]
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        //private readonly ILogger<FilmController> _logger;
        //private readonly IAuthenticationCommandService _authCommandService;
        //private readonly IAuthenticationQueryService _authQueryService;

        //public AuthenticationController(ILogger<FilmController> logger,
        //    IAuthenticationCommandService authCommandService, IAuthenticationQueryService authQueryService)
        //{
        //    _logger = logger;
        //    _authCommandService = authCommandService;
        //    _authQueryService = authQueryService;
        //}

        public AuthenticationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            //var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.BirthDate, request.Password, request.IsActor);
            var authResult = await _mediator.Send(command);
            //var authResult = _authCommandService.Register(request.FirstName, request.LastName, request.Email, request.BirthDate, request.Password, request.IsActor);
            //var response = new AuthenticationResponse(
            //    authResult.Client.Id,
            //    authResult.Client.FirstName, 
            //    authResult.Client.LastName, 
            //    authResult.Client.Email,
            //    authResult.Client.BirthDate, 
            //    authResult.Client.IsActor, 
            //    authResult.Token
            //    );
            return Ok(_mapper.Map<AuthenticationResponse>(authResult));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = _mapper.Map<LoginQuery>(request);
            //var command = new LoginQuery(request.Email, request.Password);
            var authResult = await _mediator.Send(command);
            //var response = new AuthenticationResponse(
            //    authResult.Client.Id,
            //    authResult.Client.FirstName,
            //    authResult.Client.LastName,
            //    authResult.Client.Email,
            //    authResult.Client.BirthDate,
            //    authResult.Client.IsActor,
            //    authResult.Token
            //    );
            return Ok(_mapper.Map<AuthenticationResponse>(authResult));
        }
    }
}
