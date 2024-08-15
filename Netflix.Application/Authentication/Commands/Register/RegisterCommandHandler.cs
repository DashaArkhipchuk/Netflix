using MediatR;
using Netflix.Application.Interfaces.Authentication;
using Netflix.Domain.IRepository;
using Netflix.Domain;
using Netflix.Application.Authentication.Common;
using Netflix.Application.Common.Errors;

namespace Netflix.Application.Authentication.Commands.Register
{
    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IClientRepository _clientRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IClientRepository clientRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _clientRepository = clientRepository;
        }

        public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            //Validate the user doesn`t exist
            if (_clientRepository.GetClientByEmail(command.Email) is not null)
            {
                throw new DuplicateEmailException();
            }



            //Create user (generate unique id) & persist to DB

            var hashedPass = BCrypt.Net.BCrypt.HashPassword(command.Password);

            var client = new Client
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                BirthDate = command.BirthDate,
                Password = hashedPass,
                IsActor = command.IsActor
            };

            _clientRepository.Add(client);

            //Create JWT token

            var token = _jwtTokenGenerator.GenerateToken(client);

            return new AuthenticationResult(
                 client,
                 token);
        }
    }
}
