using Mapster;
using Netflix.Application.Authentication.Common;
using Netflix.Contracts.Authentication.Common;

namespace Netflix.API.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.Client);



        }
    }
}
