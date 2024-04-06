using System;

namespace Application.Common.Exceptions;

public sealed class RefreshTokenExpiredException : Exception
{
    public RefreshTokenExpiredException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
