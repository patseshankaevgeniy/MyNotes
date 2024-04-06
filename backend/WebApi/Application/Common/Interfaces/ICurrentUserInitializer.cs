using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface ICurrentUserInitializer
{
    Task InitializeAsync(Guid userId);
}