using System.Collections.Generic;
using Formality.App.Common.Models;

namespace Formality.App.Forms.Models;

public class Form : Entity
{
    public string Name { get; set; } = string.Empty;

    public FormState StateId { get; set; } = FormState.New;

    public ICollection<FormField> Fields { get; set; } = new List<FormField>();
}
