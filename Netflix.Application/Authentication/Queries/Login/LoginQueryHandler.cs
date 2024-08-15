using MediatR;
using Netflix.Application.Interfaces.Authentication;
using Netflix.Domain.IRepository;
using Netflix.Domain;
using Netflix.Application.Authentication.Common;

namespace Netflix.Application.Authentication.Queries.Login
{
    internal class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IClientRepository _clientRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IClientRepository clientRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _clientRepository = clientRepository;
        }

        public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            //Validate the user exists
            if (_clientRepository.GetClientByEmail(query.Email) is not Client client)
            {
                throw new Exception("Client with given email does not exist");
            }

            //Validate the password is correct
            if (!BCrypt.Net.BCrypt.Verify(query.Password, client.Password))
            {
                throw new Exception("Invalid password");
            }

            //Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(client);

            return new AuthenticationResult(
                 client,
                 token);
        }
    }
}
