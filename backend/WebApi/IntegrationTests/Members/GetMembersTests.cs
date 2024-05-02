using Domain.Entities;
using FluentAssertions;
using IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebApi.IntegrationTests.Infrastructure.Client;
using Xunit;

namespace IntegrationTests.Members;

public sealed class GetMembersTests : IntegrationTest
{
    [Fact]
    public async Task GetMembers_ShouldReturnEmpty_IfUserIsNotInGroup_Async()
    {
        // Arrange
        await AuthContext.CreateUserAndAuthenticateAsync();

        // Act
        var response = await MynotesClient.GetMembersAsync();

        // Assert
        response.Should().BeEmpty();
    }

    [Fact]
    public async Task GetMembers_ShouldReturnInvitedMember_Async()
    {
        // Arrange
        var firstUserModel = await AuthContext.CreateUserAndAuthenticateAsync();
        var secondUserModel = await AuthContext.CreateUserAsync();

        await SaveDbChangesAsync(async db =>
        {
            var firstUser = await db.Users.FirstAsync(x => x.Id == firstUserModel.UserId);
            firstUser.MembershipStatus = new()
            {
                IsInviter = true,
                Status = AcceptanceStatus.Pending,
            };

            var secondUser = await db.Users.FirstAsync(x => x.Id == secondUserModel.UserId);
            secondUser.MembershipStatus = new()
            {
                IsInviter = false,
                Status = AcceptanceStatus.Pending,
            };

            db.Groups.Add(new()
            {
                Name = $"Group_{Guid.NewGuid()}",
                Users = new List<User> { firstUser, secondUser }
            });
        });

        // Act
        var response = await MynotesClient.GetMembersAsync();

        // Assert
        response.Count.Should().Be(1);
        response.First().Should().BeEquivalentTo(
            new MemberDto
            {
                UserId = secondUserModel.UserId,
                FirstName = secondUserModel.FirstName,
                SecondName = secondUserModel.SecondName,
                Email = secondUserModel.Email,
                Status = MemberStatus.WaitForApproval
            },options => options.Excluding(x => x.Id));
    }

    [Fact]
    public async Task GetMembers_ShouldReturnInviter_Async()
    {
        // Arrange
        var firstUserModel = await AuthContext.CreateUserAsync();
        var secondUserModel = await AuthContext.CreateUserAndAuthenticateAsync();

        await SaveDbChangesAsync(async db =>
        {
            var firstUser = await db.Users.FirstAsync(x => x.Id == firstUserModel.UserId);
            firstUser.MembershipStatus = new()
            {
                IsInviter = true,
                Status = AcceptanceStatus.Pending,
            };

            var secondUser = await db.Users.FirstAsync(x => x.Id == secondUserModel.UserId);
            secondUser.MembershipStatus = new()
            {
                IsInviter = false,
                Status = AcceptanceStatus.Pending,
            };

            db.Groups.Add(new()
            {
                Name = $"Group_{Guid.NewGuid()}",
                Users = new List<User> { firstUser, secondUser }
            });
        });

        // Act
        var response = await MynotesClient.GetMembersAsync();

        // Assert
        response.Count.Should().Be(1);
        response.First().Should().BeEquivalentTo(
            new MemberDto
            {
                UserId = firstUserModel.UserId,
                FirstName = firstUserModel.FirstName,
                SecondName = firstUserModel.SecondName,
                Email = firstUserModel.Email,
                Status = MemberStatus.RequiredApproval
            }, options => options.Excluding(x => x.Id));
    }
}