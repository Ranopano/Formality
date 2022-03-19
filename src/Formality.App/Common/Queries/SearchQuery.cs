using System;
using Formality.App.Common.Dto;
using MediatR;

namespace Formality.App.Common.Queries;

public class SearchQuery<T> : IRequest<T>
{
    private int _maxResult = 1000;

    public int MaxResults
    {
        get => _maxResult;
        set => _maxResult = value > 1000 ? 1000 : value;
    }

    public string? Keyword { get; set; }

    public OrderDto[] OrderBy { get; set; } = Array.Empty<OrderDto>();
}
