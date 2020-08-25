using AutoMapper;
using Formality.App.Common.Dto;
using Formality.App.Forms.Dto;
using Formality.App.Forms.Models;
using Formality.App.Submissions.Dto;
using Formality.App.Submissions.Models;

namespace Formality.App.Infrastructure
{
    public class AppDbContextProfile : Profile
    {
        public AppDbContextProfile()
        {
            CreateMap<Form, FormDto>()
                .ForMember(x => x.Fields, x => x.ExplicitExpansion());

            CreateMap<Form, FormListDto>();
            CreateMap<Form, NamedEntityDto>();

            CreateMap<FormField, FormFieldDto>();
            CreateMap<FormFieldValue, FormFieldValueDto>();

            CreateMap<Submission, SubmissionListDto>()
                .ForMember(x => x.Values, x => x.ExplicitExpansion());

            CreateMap<SubmissionValue, SubmissionValueDto>();
        }
    }
}
