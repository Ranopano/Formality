using System.Collections.Generic;
using System.Linq;
using Formality.App.Common.Dto;
using Formality.App.Forms.Models;

namespace Formality.App.Forms.Dto;

public class FormDto : NamedEntityDto
{
    public FormState StateId { get; set; }

    public IEnumerable<FormFieldDto> Fields { get; set; } = Enumerable.Empty<FormFieldDto>();
}

public class FormListDto : NamedEntityDto
{
}
