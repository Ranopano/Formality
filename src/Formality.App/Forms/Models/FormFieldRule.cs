using Formality.App.Common.Models;

namespace Formality.App.Forms.Models;

public class FormFieldRule : Entity
{
    public FieldRule Type { get; set; }

    public FormField Field { get; set; } = default!;

    public string? Data { get; set; }
}
