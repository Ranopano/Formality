namespace Formality.App.Forms.Models
{
    public class FormFieldValue
    {
        public int Id { get; set; }

        public FormField Field { get; set; } = default!;

        public string Value { get; set; } = string.Empty;
    }
}
