using System.Collections.Generic;
using System.Linq;

namespace Formality.App.Forms.Dto;

public sealed class FieldRulesDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<RuleDto> Rules { get; set; } = Enumerable.Empty<RuleDto>();
}
