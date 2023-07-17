using FluentValidation;
using FluentValidation.Results;

namespace Exam.Extensions;

public static class ValidatorExtension
{
    public static void ValidateAndThrowValidationFailure<T>(this IValidator<T> validator, T instance)
    {
        var validationResult = validator.Validate(instance);

        if (!validationResult.IsValid)
        {
            var failures = validationResult.Errors
                .Select(e => new ValidationFailure(e.PropertyName, e.ErrorMessage))
                .ToList();

            throw new ValidationException("One or more validation failures have occurred.", failures);
        }
    }
}