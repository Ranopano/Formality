using System.Collections.Generic;

namespace Formality.App.Forms.Models
{
    public class FormField
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Label { get; set; } = string.Empty;

        public string? Placeholder { get; set; }

        public Form Form { get; set; } = default!;

        public FieldType Type { get; set; } = FieldType.Text;

        public bool Deleted { get; set; }

        public ICollection<FormFieldValue> Values { get; set; } = new List<FormFieldValue>();
    }
}
