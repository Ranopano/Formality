using System.Collections.Generic;
using System.Linq;
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
}
