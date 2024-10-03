using Mapster;
using Netflix.Application.CastingCalls.Queries.GetAllCastingCalls;
using Netflix.Application.Submissions.Commands.RemoveSubmission;
using Netflix.Application.Submissions.Commands.SubmitToRole;
using Netflix.Application.Submissions.Queries.GetAllSubmissionsByCastingCall;
using Netflix.Application.Submissions.Queries.GetSubmissionsByActorId;
using Netflix.Contracts.CastingCalls;
using Netflix.Contracts.Common;
using Netflix.Contracts.Submissions;
using Netflix.Domain.Entities;

namespace Netflix.API.Common.Mapping
{
    public class SubmissionMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //(SubmitToRoleRequest, Guid, List<IFormFile>), SubmitToRoleCommand

            config.NewConfig<(SubmitToRoleRequest, Guid), SubmitToRoleCommand>()
                .Map(dest => dest, src => src.Item1)
                .Map(dest => dest.Files, src => src.Item1.Files)
                .Map(dest => dest.ClientId, src => src.Item2);

            config.NewConfig<Submission, SubmissionExtendedDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.Actor, src => src.Actor)
                .Map(dest=> dest.CastingCall, src=> src.CastingCall)
                .Map(dest => dest.SubmissionMedias, src => src.SubmissionMedias.Select(x => x.MediaUrl));

            config.NewConfig<(Guid, Guid, GetAllContentRequest), GetAllSubmissionsByCastingCallQuery>()
                .Map(dest => dest.ClientId, src => src.Item1)
                .Map(dest => dest.CastingCallId, src => src.Item2)
                .Map(dest => dest.Skip, src => src.Item3.Skip)
                .Map(dest => dest.Take, src => src.Item3.Take);

            config.NewConfig<(Guid, Guid), RemoveSubmissionCommand>()
                .Map(dest => dest.ClientId, src => src.Item1)
                .Map(dest => dest.SubmissionId, src => src.Item2);

            config.NewConfig<Submission, SubmissionDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.ActorName, src => src.Actor.StageName)
                .Map(dest => dest.CastingCallId, src => src.CastingId)
                .Map(dest => dest.SubmissionMedias, src => src.SubmissionMedias.Select(x => x.MediaUrl));

            config.NewConfig<(Guid, GetAllContentRequest), GetSubmissionsByActorIdQuery>()
                .Map(dest => dest.Skip, src => src.Item2.Skip)
                .Map(dest => dest.Take, src => src.Item2.Take)
                .Map(dest => dest.ClientId, src => src.Item1);
        }
    }
}
