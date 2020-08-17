using Formality.App.Common.Models;
using System.Collections.Generic;

namespace Formality.App.Forms.Models
{
    public class Form : Entity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<FormField> Fields { get; set; } = new List<FormField>();
    }
}
