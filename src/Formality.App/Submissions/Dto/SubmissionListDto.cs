using System;
using System.Collections.Generic;
using Formality.App.Common.Dto;

namespace Formality.App.Submissions.Dto
{
    public class SubmissionListDto
    {
        public int Id { get; }

        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;

        public NamedEntityDto Form { get; set; } = default!;

        public ICollection<SubmissionValueDto> Values { get; set; } = new List<SubmissionValueDto>();
    }
}
