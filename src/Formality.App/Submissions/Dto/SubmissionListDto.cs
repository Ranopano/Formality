using System;
using System.Collections.Generic;

namespace Formality.App.Submissions.Dto
{
    public class SubmissionListDto
    {
        public int Id { get; }

        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;

        public ICollection<SubmissionValueDto> Values { get; set; } = new List<SubmissionValueDto>();
    }
}
