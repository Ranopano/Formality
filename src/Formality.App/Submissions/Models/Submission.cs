using System;
using System.Collections.Generic;
using Formality.App.Common.Models;
using Formality.App.Forms.Models;

namespace Formality.App.Submissions.Models
{
    public class Submission : Entity
    {
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;

        public Form Form { get; set; } = default!;

        public ICollection<SubmissionValue> Values { get; set; } = new List<SubmissionValue>();
    }
}
