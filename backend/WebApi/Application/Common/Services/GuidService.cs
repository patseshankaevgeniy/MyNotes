using Application.Common.Interfaces;
using System;

namespace Application.Common.Services;

public sealed class GuidService : IGuidService
{
    public Guid NewGuid() => Guid.NewGuid();
}