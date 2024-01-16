namespace Customer.Application.Exceptions;

using ApplicationException = Core.Exceptions.ApplicationException;

public sealed class ValidationException : ApplicationException
{
    public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
        : base("Validation Failure", "One or more validation errors occurred")
        => ErrorsDictionary = errorsDictionary;
    
    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
}