﻿using FluentValidation.Results;

namespace CleanArchitecture.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public ValidationException(string[] errors) : this()
    {
        Dictionary<string, string[]> validationDictionary = new();
        for (int i = 0; i < errors.Length; i++)
        {
            validationDictionary.Add(i.ToString(), new[] { errors[i] });
        }

        Errors = validationDictionary;
    }

    public IDictionary<string, string[]> Errors { get; }
}