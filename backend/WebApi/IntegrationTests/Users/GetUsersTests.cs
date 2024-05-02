using Application.Users.Models;
using Domain.Entities;
using FluentAssertions;
using IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebApi.IntegrationTests.Infrastructure.Client;
using Xunit;

namespace IntegrationTests.Users;

public sealed class GetUsersTests : IntegrationTest
{
    [Fact]
    public async Task GetUsers_ShouldReturnAllUser_Async()
    {
        // Arrange
        var firstUserModel = await AuthContext.CreateUserAndAuthenticateAsync();
        var secondUserModel = await AuthContext.CreateUserAsync();

        // Act
        var users = await MynotesClient.GetUsersAsync(hasMembers: null, searchPattern: null);

        // Assert
        users.Count.Should().Be(2);
        foreach (var userModel in new[] { firstUserModel, secondUserModel })
        {
            var userDto = users.First(u => u.UserId == userModel.UserId);
            CheckUserDtoEqualsUserModel(userDto, userModel);
        }
    }

    [Fact]
    public async Task GetUsers_ShouldReturnUserByName_Async()
    {
        // Arrange
        var firstUserModel = await AuthContext.CreateUserAndAuthenticateAsync();
        await AuthContext.CreateUserAsync();

        // Act
        var users = await MynotesClient.GetUsersAsync(hasMembers: null, searchPattern: firstUserModel.FirstName);

        // Assert
        users.Count.Should().Be(1);
        CheckUserDtoEqualsUserModel(users.First(), firstUserModel);
    }

    [Fact]
    public async Task GetUsers_ShouldReturnUsers_IfTheyDontHaveMembers_Async()
    {
        // Arrange
        var firstUserModel = await AuthContext.CreateUserAsync();
        var secondUserModel = await AuthContext.CreateUserAsync();
        var thirdUserModel = await AuthContext.CreateUserAndAuthenticateAsync();

        await SaveDbChangesAsync(async db =>
        {
            var firstUser = await db.Users.FirstAsync(x => x.Id == firstUserModel.UserId);
            firstUser.MembershipStatus = new()
            {
                Status = AcceptanceStatus.Completed,
            };

            var secondUser = await db.Users.FirstAsync(x => x.Id == secondUserModel.UserId);
            secondUser.MembershipStatus = new()
            {
                IsInviter = false,
                Status = AcceptanceStatus.Completed,
            };

            db.Groups.Add(new()
            {
                Name = $"Group_{Guid.NewGuid()}",
                Users = new List<User> { firstUser, secondUser }
            });
        });

        // Act
        var users = await MynotesClient.GetUsersAsync(hasMembers: false, searchPattern: null);

        // Assert
        users.Count.Should().Be(1);
        CheckUserDtoEqualsUserModel(users.First(), thirdUserModel);
    }

    [Fact]
    public async Task GetUsers_ShouldExludeCurrent_Async()
    {
        // Arrange
        var firstUserModel = await AuthContext.CreateUserAndAuthenticateAsync();
        var secondUserModel = await AuthContext.CreateUserAsync();

        // Act
        var users = await MynotesClient.GetUsersAsync(excludeCurrent: true);

        // Assert
        users.Count.Should().Be(1);
        CheckUserDtoEqualsUserModel(users.First(), secondUserModel);
    }

    private void CheckUserDtoEqualsUserModel(UserDto dto, UserModel model)
    {
        dto.Should().BeEquivalentTo(
            new UserDto
            {
                UserId = model.UserId,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Email = model.Email,
                LanguageId = model.LanguageId
            });
    }
}