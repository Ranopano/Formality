namespace Formality.App.Forms.Models;

public enum FieldRule
{
    Unknown,
    Required,
    Length,
}

public class LengthData
{
    public int MinLength { get; set; }

    public int MaxLength { get; set; }
}
