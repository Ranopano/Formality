using Formality.App.Common.Models;
using Formality.App.Forms.Models;

namespace Formality.App.Submissions.Models
{
    public class SubmissionValue : Entity
    {
        public int FieldId { get; set; }

        public int SubmissionId { get; set; }

        public Submission Submission { get; set; } = default!;

        public FormField Field { get; set; } = default!;

        /// <summary>
        /// Type of the field at the time of submission.
        /// </summary>
        public FieldType Type { get; set; }

        public string Value { get; set; } = string.Empty;
    }
}
