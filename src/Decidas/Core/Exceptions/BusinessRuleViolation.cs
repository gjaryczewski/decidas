namespace Decidas.Core.Exceptions;

public class BusinessRuleViolation : Exception
{
    public BusinessRuleViolation(string message) : base(message)
    {
    }

    public BusinessRuleViolation(string source, string message) : base(message)
    {
    }
}