using Application.Auth.Models;
using FluentAssertions;
using IntegrationTests.Infrastructure;
using Persistence.Infrastructure;
using System.Net;
using WebApi.IntegrationTests.Infrastructure.Client;
using Xunit;

namespace IntegrationTests.Auth;


public class AuthControllerTests : IntegrationTest
{
    [Fact]
    public async Task LogIn_ShouldReturnOk_SucceededAndToken_Async()
    {
        // Arrange
        
       var userModel = await AuthContext.CreateUserAsync();

        // Act

        var loginDto = new LogInDto
        {
            Email = userModel.Email,
            Password = SeedData.DefaultPassword
        };

        var response = await MynotesClient.LogInAsync(loginDto);

        // Assert

        response.Succeeded.Should().BeTrue();
        response.FailureReason.Should().BeNull();
        response.AccessToken.Should().NotBeNullOrEmpty();

    }

    [Fact]
    public async Task LogIn_ShouldReturn200_NotSuccededAndReason_Async()
    {
        // Act
        var loginDto = new LogInDto
        {
            Email = "any@gmail.com",
            Password = "password"
        };

        var response = await MynotesClient.LogInAsync(loginDto);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.AccessToken.Should().BeNull();
        response.FailureReason.Should().Be((int)FailureReason.UserNotFound);
    }

    [Fact]
    public async Task LogIn_ShouldReturn400_ValidationDescription_Async()
    {
        // Act
        var loginDto = new LogInDto
        {
            Email = string.Empty,
            Password = null
        };

        var task = MynotesClient.LogInAsync(loginDto);

        // Assert
        var apiExceptionDto = await Assert.ThrowsAsync<ApiException<ErrorDto>>(() => task);
        apiExceptionDto.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        apiExceptionDto.Result.Should().NotBeNull();
    }
}
