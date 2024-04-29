using Application.Common.Interfaces;
using Domain.Entities;
using System;

namespace Application.Common.Services;

public sealed class DateTimeService : IDateTimeService
{
    public const double DAYS_FOR_LOW_PRIORITY = 7;
    public const double DAYS_FOR_MEDIUM_PRIORITY = 3;
    public const double DAYS_FOR_HIGH_PRIORITY = 1;

    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime Today => DateTime.UtcNow.Date;

    public DateTime SetCompetionNoteTime(NotePriority priority)
    {
        var competionNoteTime = UtcNow;

        if (priority == NotePriority.Low)
        {
            competionNoteTime = competionNoteTime.AddDays(DAYS_FOR_LOW_PRIORITY);
        }
        if (priority == NotePriority.Medium)
        {
            competionNoteTime = competionNoteTime.AddDays(DAYS_FOR_MEDIUM_PRIORITY);
        }
        if (priority == NotePriority.High)
        {
            competionNoteTime = competionNoteTime.AddDays(DAYS_FOR_HIGH_PRIORITY);
        }
        return competionNoteTime;
    }
}