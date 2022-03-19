using Formality.App.Forms.Models;

namespace Formality.App.Submissions.Dto;

public class SubmissionValueDto
{
    public int? Id { get; set; }

    public int FieldId { get; set; }

    public FieldType Type { get; set; }

    public string Value { get; set; } = string.Empty;
}
