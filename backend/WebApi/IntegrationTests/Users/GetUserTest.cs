using FluentAssertions;
using IntegrationTests.Infrastructure;
using WebApi.IntegrationTests.Infrastructure.Client;
using Xunit;

namespace IntegrationTests.Users;

public sealed class GetCurrentUserTest : IntegrationTest
{
    [Fact]
    public async Task GetUser_ShouldReturnCurrentUser_Async()
    {
        // Arrange
        var currentUserModel = await AuthContext.CreateUserAndAuthenticateAsync();

        // Act
        var userDto = await MynotesClient.GetCurrentUserAsync();

        // Assert
        userDto.Should().NotBeNull();
        userDto.Should().BeEquivalentTo(
            new UserDto
            {
                UserId = currentUserModel.UserId,
                FirstName = currentUserModel.FirstName,
                SecondName = currentUserModel.SecondName,
                Email = currentUserModel.Email,
                LanguageId = currentUserModel.LanguageId
            });
    }
}