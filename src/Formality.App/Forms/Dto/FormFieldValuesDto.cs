using System.Collections.Generic;
using System.Linq;

namespace Formality.App.Forms.Dto;

public sealed class FormFieldValuesDto
{
    public int FormId { get; set; }

    public IEnumerable<FieldValueDto> Values { get; set; } = Enumerable.Empty<FieldValueDto>();
}
