using FluentValidation.Results;

namespace Exam.Web.Common;

public class GenericResponse<T>
{
    public GenericError? Error { get; set; }
    public T? Result { get; set; }
    public bool Success { get; set; } = default;
}

public class GenericError
{
    public int Code { get; set; }
    public string? Details { get; set; } = default;
    public string? Message { get; set; } = default;
    public List<ValidationFailure>? ValidationErrors { get; set; }
}