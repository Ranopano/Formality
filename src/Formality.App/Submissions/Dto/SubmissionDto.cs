using System;
using System.Collections.Generic;
using Formality.App.Common.Dto;

namespace Formality.App.Submissions.Dto
{
    public class SubmissionDto : EntityDto
    {
        public int FormId { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;

        public ICollection<SubmissionValueDto> Values { get; set; } = new List<SubmissionValueDto>();
    }

    public class SubmissionListDto : EntityDto
    {
        public NamedEntityDto Form { get; set; } = default!;

        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
    }
}
