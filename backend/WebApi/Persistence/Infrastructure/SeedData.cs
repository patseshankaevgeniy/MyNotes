using Application.Common.Interfaces;
using Application.Users.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Infrastructure;

public static class SeedData
{
    public static Guid DefaultImageId => Guid.Parse("d49d53cf-adb1-4507-b7c9-81bec78db9b6");
    public static Guid DefaultLanguageId => Guid.Parse("d49d53cf-adb3-4507-b7c9-81bec78db9b6");
    public static string DefaultPassword => "1234";

    public static void ApplySeedData(this ModelBuilder modelBuilder)
    {
        
        var languages = CreateLanguages();

        modelBuilder.Entity<Language>().HasData(languages);

        
    }

    public static async Task ApplySeedDataAsync(IServiceProvider serviceProvider)
    {
        var db = serviceProvider.GetRequiredService<IApplicationDbContext>();
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        if (await db.Users.AnyAsync())
        {
            return;
        }

        await CreateDefaultUsersAsync(mediator);
    }

    private static IEnumerable<Language> CreateLanguages()
    {
        return new List<Language>
        {
            new Language { Id = DefaultLanguageId, Value = "ru" },
            new Language { Id = Guid.Parse("d49d53cf-adb3-4507-b7c9-81bec78db9b9"), Value = "eng" }
        };
    }

    private static async Task CreateDefaultUsersAsync(IMediator mediator)
    {
        await mediator.Send(new CreateUserCommand
        {
            UserName = "pateshvaler@gmail.com",
            FirstName = "Lera",
            SecondName = "Moskaliova",
            Email = "pateshvaler@gmail.com"
        });

        await mediator.Send(new CreateUserCommand
        {
            UserName = "dzianis.maskaliou@gmail.com",
            FirstName = "Denis",
            SecondName = "Maskaliou",
            Email = "dzianis.maskaliou@gmail.com"
        });

        await mediator.Send(new CreateUserCommand
        {
            UserName = "patseshanka.eugeniy@gmail.com",
            FirstName = "Eugeniy",
            SecondName = "Patseshanka",
            Email = "gzaytsev2000@gmail.com",
            Password = "qwerty"
        });
    }
}