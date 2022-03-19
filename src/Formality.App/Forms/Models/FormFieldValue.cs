using Formality.App.Common.Models;

namespace Formality.App.Forms.Models;

public class FormFieldValue : Entity
{
    public FormField Field { get; set; } = default!;

    public string Value { get; set; } = string.Empty;
}
