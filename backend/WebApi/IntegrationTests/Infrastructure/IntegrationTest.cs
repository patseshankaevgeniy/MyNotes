using Application.Common.Interfaces;
using IntegrationTests.Infrastructure.Auth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence;
using WebApi;
using WebApi.IntegrationTests.Infrastructure.Client;
using Xunit;
using Mocks = IntegrationTests.Infrastructure.Models.Mocks;

namespace IntegrationTests.Infrastructure;

public abstract class IntegrationTest : IAsyncLifetime
{
    private TestWebApplicationFactory<Startup> _factory = default!;

    protected HttpClient HttpClient { get; private set; } = default!;
    protected IMynotesClient MynotesClient { get; private set; } = default!;

    protected AuthContext AuthContext { get; private set; } = default!;
    // protected GoogleServicesContext GoogleServicesContext { get; private set; } = default!;

    protected IServiceProvider ServiceProvider { get; private set; } = default!;

    protected Mocks Mocks { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        // DependencyInjection.ShouldSkipRegistration = true;

        Mocks = new Mocks();

        // var massageSenderMock = new Mock<ITelegramMessageSender>();

        _factory = new TestWebApplicationFactory<Startup>
        {
            ConfigureServices = services =>
            {
                // IGuidService
                services.RemoveAll(typeof(IGuidService));
                services.AddSingleton(Mocks.GuidService.Object);

                // IDateTime
                services.RemoveAll(typeof(IDateTimeService));
                services.AddSingleton(Mocks.DateTimeService.Object);

                // TelegramMessageSender
                //services.RemoveAll(typeof(ITelegramMessageSender));
                //services.RemoveAll(typeof(TelegramMessageSender));
                //services.AddSingleton(massageSenderMock.Object);

                // IGoogleSheetsService
                // services.RemoveAll(typeof(IGoogleSheetsService));
                // services.AddScoped(_ => Mocks.GoogleSheetsService.Object);

                // GoogleTokenVerificator
                //services.RemoveAll(typeof(IGoogleTokenVerificator));
                //services.AddSingleton(Mocks.GoogleTokenVerificator.Object);

                // GoogleImageService
                //services.RemoveAll(typeof(IGoogleImageService));
                //services.AddScoped(_ => Mocks.GoogleImageService.Object);

                // ImagesStorage
                //services.RemoveAll(typeof(IImagesStorage));
                //services.AddScoped(_ => Mocks.ImagesStorage.Object);

                // IOptions<GeneratePurchasesOptions>
                //services.RemoveAll(typeof(IOptions<GenerateFakePurchasesOptions>));
                //services.AddScoped(_ => Mocks.GeneratePurchasesOptions.Object);

                //services.AddScoped(_ => Mocks.BackgroundJobScheduler.Object);
            }
        };

        HttpClient = _factory.CreateClient(new() { AllowAutoRedirect = false });
        ServiceProvider = _factory.Services;

        MynotesClient = new MynotesClient(HttpClient);
        //TelegramContext = new TelegramContext(ServiceProvider, massageSenderMock);
        //BackgroundJobsContext = new BackgroundJobsContext(ServiceProvider);
        AuthContext = new AuthContext(HttpClient, MynotesClient, ServiceProvider);
        //GoogleServicesContext = new GoogleServicesContext(ServiceProvider);

        // Migrate db
        using var scope = ServiceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        HttpClient?.Dispose();
        _factory.Dispose();
        return Task.CompletedTask;
    }

    protected async Task AddSeedDataAsync<T>(T[] entities)
    {
        using var scope = ServiceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        foreach (var entity in entities)
        {
            await db.AddAsync(entity!);
        }

        await db.SaveChangesAsync();
    }

    protected async Task SaveDbChangesAsync(Func<ApplicationDbContext, Task> dbCommand)
    {
        using var scope = ServiceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbCommand(db);
        await db.SaveChangesAsync();
    }

    protected async Task CheckInDbAsync(Func<ApplicationDbContext, Task> predicate)
    {
        using var scope = ServiceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await predicate(db);
    }

    protected async Task GetFromDbAsync(Func<ApplicationDbContext, Task> query)
    {
        using var scope = ServiceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await query(db);
    }
}