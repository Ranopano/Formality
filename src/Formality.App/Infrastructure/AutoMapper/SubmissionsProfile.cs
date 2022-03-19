using AutoMapper;
using Formality.App.Forms.Dto;
using Formality.App.Submissions.Commands;
using Formality.App.Submissions.Dto;
using Formality.App.Submissions.Models;

namespace Formality.App.Infrastructure.AutoMapper;

public class SubmissionsProfile : Profile
{
    public SubmissionsProfile()
    {
        CreateMap<Submission, SubmissionDto>()
            .ForMember(x => x.Values, x => x.ExplicitExpansion());
        CreateMap<Submission, SubmissionListDto>();

        CreateMap<SubmissionValue, SubmissionValueDto>();
        CreateMap<SubmissionValueDto, FieldValueDto>();

        CreateMap<AddSubmissionCommand, FormFieldValuesDto>();
    }
}
