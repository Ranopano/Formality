using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Formality.App.Common.Dto;

namespace Formality.App.Forms.Dto
{
    public class FormDto : NamedEntityDto
    {
        public IEnumerable<FormFieldDto> Fields { get; set; } = Enumerable.Empty<FormFieldDto>();
    }

    public class FormListDto : NamedEntityDto
    {
    }

    public class FormDtoValidator : AbstractValidator<FormDto>
    {
        public FormDtoValidator(bool checkName = false)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256)
                .Matches(@"^[\w\d-]+$");

            RuleFor(x => x.Name)
                .NotEmpty().When(_ => checkName)
                .MaximumLength(256);
        }
    }
}
