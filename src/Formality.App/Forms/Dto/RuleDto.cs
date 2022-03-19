using Formality.App.Forms.Models;

namespace Formality.App.Forms.Dto;

public class RuleDto
{
    public FieldRule Type { get; set; }

    public string? Data { get; set; }
}
