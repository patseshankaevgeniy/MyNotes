using System;

namespace Application.Common.Interfaces;

public interface IConstantsService
{
    Guid DefaultImageId { get; }
    Guid DefaultLanguageId { get; }
    string DefaultPassword { get; }
}