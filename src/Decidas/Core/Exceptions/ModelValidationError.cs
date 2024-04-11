namespace Decidas.Core.Exceptions;

public class ModelValidationError : Exception
{
    public ModelValidationError(string message) : base(message)
    {
    }

    public ModelValidationError(string source, string message) : base(message)
    {
    }
}