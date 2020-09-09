using System;
using Formality.App.Common.Dto;
using Formality.App.Forms.Models;

namespace Formality.App.Forms.Dto
{
    public class FormFieldDto : NamedEntityDto
    {
        public string Label { get; set; } = string.Empty;

        public string? Placeholder { get; set; }

        public bool Deleted { get; set; }

        public FieldType Type { get; set; }

        public FormFieldRuleDto[] Rules { get; set; } = Array.Empty<FormFieldRuleDto>();

        public FormFieldValueDto[] Values { get; set; } = Array.Empty<FormFieldValueDto>();
    }
}
