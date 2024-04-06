using Application.Common.Interfaces;
using System;

namespace Application.Common.Services;

public sealed class ConstantsService : IConstantsService
{
    public Guid DefaultImageId => Guid.Parse("d49d53cf-adb1-4507-b7c9-81bec78db9b6");
    public Guid DefaultLanguageId => Guid.Parse("d49d53cf-adb3-4507-b7c9-81bec78db9b6");
    public string DefaultPassword => "1234";
}