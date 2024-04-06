using System;

namespace Application.Common.Exceptions;

public sealed class WrongSetupException : Exception
{
    public WrongSetupException(string message)
        : base(message)
    {
    }
}