using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Formality.App.Forms.Dto;
using Formality.App.Forms.Models;
using Formality.App.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Formality.App.Forms.Validation;

public sealed class FormFieldsValidator : AbstractValidator<FormFieldValuesDto>
{
    private readonly IReadOnlyAppDbContext _context;

    private readonly IMapper _mapper;

    public FormFieldsValidator(IReadOnlyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public override async Task<ValidationResult> ValidateAsync(
        ValidationContext<FormFieldValuesDto> context,
        CancellationToken cancellationToken = default)
    {
        var formFields = context.InstanceToValidate;

        var fieldsQuery = _context.FormFields
            .Where(x => x.Form.Id == formFields.FormId && !x.Deleted);

        var fields = await _mapper
            .ProjectTo<FieldRulesDto>(fieldsQuery)
            .ToArrayAsync(cancellationToken);

        foreach (var field in fields)
        {
            var value = formFields.Values
                .Where(x => x.FieldId == field.Id)
                .Select(x => x.Value)
                .FirstOrDefault(string.Empty);

            var validator = new FormFieldValueValidator(field);
            var result = validator.Validate(value);

            foreach (var error in result.Errors)
            {
                context.AddFailure(error);
            }
        }

        return await base.ValidateAsync(context, cancellationToken);
    }
}

public sealed class FormFieldValueValidator : AbstractValidator<string>
{
    public FormFieldValueValidator() // for DI scope check
    {
    }

    public FormFieldValueValidator(FieldRulesDto field)
    {
        var rules = new Dictionary<FieldRule, Action<FieldRulesDto, RuleDto>>
        {
            { FieldRule.Required, AddRequiredRule },
            { FieldRule.Length, AddLengthRule }
        };

        foreach (var rule in field.Rules)
        {
            var addRule = rules[rule.Type];
            addRule(field, rule);
        }
    }

    private void AddRequiredRule(FieldRulesDto field, RuleDto rule)
    {
        RuleFor(x => x).NotEmpty().WithName(field.Name);
    }

    private void AddLengthRule(FieldRulesDto field, RuleDto rule)
    {
        if (rule.Data is null)
        {
            throw new NullReferenceException(nameof(rule.Data));
        }

        var options = JsonSerializer.Deserialize<LengthData>(rule.Data, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        RuleFor(x => x)
            .MinimumLength(options?.MinLength ?? int.MinValue)
            .MaximumLength(options?.MaxLength ?? int.MaxValue)
            .Unless(string.IsNullOrEmpty)
            .WithName(field.Name);
    }
}
