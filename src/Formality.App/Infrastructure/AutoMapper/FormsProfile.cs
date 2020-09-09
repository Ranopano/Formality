using AutoMapper;
using Formality.App.Common.Dto;
using Formality.App.Forms.Dto;
using Formality.App.Forms.Models;

namespace Formality.App.Infrastructure.AutoMapper
{
    public class FormsProfile : Profile
    {
        public FormsProfile()
        {
            CreateMap<Form, FormDto>()
                .ForMember(x => x.Fields, x => x.ExplicitExpansion());
            CreateMap<Form, FormListDto>();
            CreateMap<Form, NamedEntityDto>();

            CreateMap<FormField, FormFieldDto>();
            CreateMap<FormField, FieldRulesDto>();

            CreateMap<FormFieldRule, FormFieldRuleDto>();
            CreateMap<FormFieldRule, RuleDto>();

            CreateMap<FormFieldValue, FormFieldValueDto>();
        }
    }
}
