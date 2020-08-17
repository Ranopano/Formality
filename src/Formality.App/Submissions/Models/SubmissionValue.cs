using Formality.App.Forms.Models;

namespace Formality.App.Submissions.Models
{
    public class SubmissionValue
    {
        public int Id { get; set; }

        public Submission Submission { get; set; } = default!;

        public FormField Field { get; set; } = default!;

        public FieldType Type { get; set; } = FieldType.Text;

        public string Value { get; set; } = string.Empty;
    }
}
