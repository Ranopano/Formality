using System.Collections.Generic;
using Formality.App.Common.Models;

namespace Formality.App.Forms.Models;

public class FormField : Entity
{
    public string Name { get; set; } = string.Empty;

    public string Label { get; set; } = string.Empty;

    public string? Placeholder { get; set; }

    public Form Form { get; set; } = default!;

    public FieldType Type { get; set; } = FieldType.Unknown;

    public bool Deleted { get; set; }

    public ICollection<FormFieldRule> Rules { get; set; } = new List<FormFieldRule>();

    public ICollection<FormFieldValue> Values { get; set; } = new List<FormFieldValue>();
}
