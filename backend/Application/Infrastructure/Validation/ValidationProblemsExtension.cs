namespace Application.Infrastructure.Validation;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using System.Net;

public static class ValidationProblemsExtension
{
    public static ProblemDetails ToProblemDetails(this ValidationResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return new()
        {
            Title = "Validation failed",
            Status = (int)HttpStatusCode.BadRequest,
            Detail = string.Concat(result.Errors.Select(x => x.ErrorMessage)),
        };
    }
}
