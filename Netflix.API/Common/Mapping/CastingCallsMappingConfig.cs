using Mapster;
using Microsoft.EntityFrameworkCore.Design;
using Netflix.Application.CastingCalls.Commands.CreateCastingCall;
using Netflix.Application.CastingCalls.Commands.RemoveCastingCall;
using Netflix.Application.CastingCalls.Commands.UpdateCastingCall;
using Netflix.Application.CastingCalls.Queries.GetAllCastingCalls;
using Netflix.Application.CastingCalls.Queries.GetAllCastingCallsByDirector;
using Netflix.Application.Common.Content;
using Netflix.Application.Submissions.Commands.RemoveSubmission;
using Netflix.Contracts.CastingCalls;
using Netflix.Contracts.CastingCalls.CreateCastingCall;
using Netflix.Contracts.Common;
using Netflix.Domain.Entities;

namespace Netflix.API.Common.Mapping
{
    public class CastingCallsMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(GetAllContentRequest, CastingCriteria), GetAllCastingCallsQuery>()
                .Map(dest => dest.Criteria, src => src.Item2)
                .Map(dest => dest.Skip, src => src.Item1.Skip)
                .Map(dest => dest.Take, src => src.Item1.Take);


            config.NewConfig<(Guid, GetAllContentRequest, CastingCriteria), GetAllCastingCallsByDirectorQuery>()
                .Map(dest => dest.Criteria, src => src.Item3)
                .Map(dest => dest.Skip, src => src.Item2.Skip)
                .Map(dest => dest.Take, src => src.Item2.Take)
                .Map(dest => dest.ClientId, src => src.Item1);

            config.NewConfig<CastingCall, CastingCallDto>()
                .Map(dest => dest.Genders, src => src.IsAnyGenderAccepted ? new List<string>(new string[] { "All genders" }) : src.Genders.Select(x => x.GenderName).ToList())
                .Map(dest => dest.Locations, src => src.Locations.Select(x => $"{x.LocationName}, {x.RegionName}").ToList())
                .Map(dest => dest.ProjectType, src => src.ProjectType.ProjectTypeName)
                .Map(dest => dest.RoleType, src => src.RoleType.RoleTypeName);

            config.NewConfig<CastingCall, CastingCallExtendedDto>()
                .Map(dest => dest.EthnicAppearances, src => src.IsAnyEthnicAppearanceAccepted ? new List<string>(new string[] { "All ethnic appearances" }) : src.EthnicAppearances.Select(x => x.EthnicAppearanceName).ToList())
                .Map(dest => dest.Genders, src => src.IsAnyGenderAccepted ? new List<string>(new string[] { "All genders" }) : src.Genders.Select(x => x.GenderName).ToList())
                .Map(dest => dest.Locations, src => src.Locations.Select(x => $"{x.LocationName}, {x.RegionName}").ToList())
                .Map(dest => dest.ProjectType, src => src.ProjectType.ProjectTypeName)
                .Map(dest => dest.RoleType, src => src.RoleType.RoleTypeName);

            config.NewConfig<(Guid, Guid), RemoveCastingCallCommand>()
               .Map(dest => dest.ClientId, src => src.Item1)
               .Map(dest => dest.CastingId, src => src.Item2);

            config.NewConfig<Guid, GetContentByIdQuery<CastingCall>>()
               .Map(dest => dest.Id, src => src);

            config.NewConfig<Audition, AuditionDto>()
                .Map(dest => dest.DateFrom, src => src.DateFrom)
                .Map(dest => dest.DateTo, src => src.DateTo)
                .Map(dest => dest.Location, src => src.Location.LocationName + ", " + src.Location.RegionName);

            config.NewConfig<(Guid, CreateCastingCallRequest), CreateCastingCallCommand>()
                .Map(dest => dest, src => src.Item2)
                .Map(dest => dest.CreatedByDirectorId, src => src.Item1);

            config.NewConfig<(Guid, Guid, CreateCastingCallRequest), UpdateCastingCallCommand>()
                .Map(dest => dest, src => src.Item3)
                .Map(dest => dest.ClientId, src => src.Item1)
                .Map(dest => dest.CastingCallId, src => src.Item2);

        }
    }
}
