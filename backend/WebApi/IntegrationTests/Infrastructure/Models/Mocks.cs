using Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Moq;

namespace IntegrationTests.Infrastructure.Models;

public sealed class Mocks
{
    public Mock<IGuidService> GuidService { get; }
    public Mock<IDateTimeService> DateTimeService { get; }
   // public Mock<IGoogleTokenVerificator> GoogleTokenVerificator { get; }
   // public Mock<IGoogleImageService> GoogleImageService { get; }
   // public Mock<IImagesStorage> ImagesStorage { get; }
    //public Mock<IGoogleSheetsService> GoogleSheetsService { get; }
    //public Mock<IOptions<GenerateFakeNotessOptions>> GenerateNotesOptions { get; }

    public Mocks()
    {
        GuidService = new Mock<IGuidService>();
        GuidService.Setup(x => x.NewGuid()).Returns(() => Guid.NewGuid());

        DateTimeService = new Mock<IDateTimeService>();
        DateTimeService.SetupNow();

        //GoogleTokenVerificator = new Mock<IGoogleTokenVerificator>();

        //GoogleImageService = new Mock<IGoogleImageService>();
        //ImagesStorage = new Mock<IImagesStorage>();

        //BackgroundJobScheduler = new Mock<IBackgroundJobScheduler>();
        //GoogleSheetsService = new Mock<IGoogleSheetsService>();
        //GeneratePurchasesOptions = new Mock<IOptions<GenerateFakePurchasesOptions>>();
    }
}

public static class MockExtensions
{
    public static Guid SetupGuid(this Mock<IGuidService> guidService)
    {
        var guid = Guid.NewGuid();
        guidService.Setup(x => x.NewGuid()).Returns(guid);

        return guid;
    }

    public static DateTime SetupUtcNow(this Mock<IDateTimeService> dateService)
    {
        var dateTime = DateTime.UtcNow;
        dateService.SetupGet(x => x.UtcNow).Returns(dateTime);

        return dateTime;
    }

    public static DateTime SetupNow(this Mock<IDateTimeService> dateService)
    {
        var dateTime = DateTime.Now;
        dateService.SetupGet(x => x.Now).Returns(dateTime);

        return dateTime;
    }
}