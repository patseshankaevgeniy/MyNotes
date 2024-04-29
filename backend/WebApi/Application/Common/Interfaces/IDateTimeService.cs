using Domain.Entities;
using System;

namespace Application.Common.Interfaces;

public interface IDateTimeService
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
    DateTime Today { get; }
    DateTime SetCompetionNoteTime(NotePriority priority);
}