using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Formality.App.Forms.Models;
using Microsoft.EntityFrameworkCore;

namespace Formality.App.Infrastructure;

public class AppDbContextSeed
{
    private readonly AppDbContext _context;

    public AppDbContextSeed(AppDbContext context)
    {
        _context = context;
    }

    public virtual async Task SeedAsync()
    {
        if (!await _context.Forms.AnyAsync())
        {
            await AddDefaultForm();
        }
    }

    private async Task AddDefaultForm()
    {
        var form = new Form
        {
            Name = "Default Form",
            StateId = FormState.Actual,
            Fields = new List<FormField>
            {
                new FormField
                {
                    Name = "name",
                    Label = "Name",
                    Placeholder = "Please enter your name",
                    Type = FieldType.SingleLineText,
                    Rules = new List<FormFieldRule>
                    {
                        new FormFieldRule
                        {
                            Type = FieldRule.Required
                        },
                    },
                },
                new FormField
                {
                    Name = "about",
                    Label = "About",
                    Placeholder = "Please tell us something about yourself",
                    Type = FieldType.MultiLineText,
                    Rules = new List<FormFieldRule>
                    {
                        new FormFieldRule
                        {
                            Type = FieldRule.Length,
                            Data = JsonSerializer.Serialize(
                                new LengthData
                                {
                                    MaxLength = 200,
                                    MinLength = 2,
                                },
                                new JsonSerializerOptions
                                {
                                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                                }),
                        },
                    },
                },
                new FormField
                {
                    Name = "quantity",
                    Label = "Quantity",
                    Type = FieldType.Dropdown,
                    Rules = new List<FormFieldRule>
                    {
                        new FormFieldRule
                        {
                            Type = FieldRule.Required
                        },
                    },
                    Values = new List<FormFieldValue>
                    {
                        new FormFieldValue
                        {
                            Value = "One",
                        },
                        new FormFieldValue
                        {
                            Value = "Hundred",
                        },
                        new FormFieldValue
                        {
                            Value = "Thousand",
                        },
                    },
                },
            },
        };

        _context.Add(form);

        await _context.SaveChangesAsync();
    }
}
