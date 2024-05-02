using Domain.Entities;
using FluentAssertions;
using IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests.Members;

public sealed class AcceptInvitationTests : IntegrationTest
{
    [Fact]
    public async Task AcceptInvitation_ShouldSetStatusesForBothMembers_Async()
    {
        // Arrange
        var currentUserModel = await AuthContext.CreateUserAndAuthenticateAsync();
        var invitedUserModel = await AuthContext.CreateUserAsync();

        await SaveDbChangesAsync(async db =>
        {
            var currentUser = await db.Users.FirstAsync(x => x.Id == currentUserModel.UserId);
            currentUser.MembershipStatus = new()
            {
                IsInviter = true,
                Status = AcceptanceStatus.Pending,
            };

            var invitedUser = await db.Users.FirstAsync(x => x.Id == invitedUserModel.UserId);
            invitedUser.MembershipStatus = new()
            {
                IsInviter = false,
                Status = AcceptanceStatus.Pending,
            };

            db.Groups.Add(new()
            {
                Name = $"Group_{Guid.NewGuid()}",
                Users = new List<User> { currentUser, invitedUser }
            });
        });

        // Act
        await MynotesClient.AcceptInvitationAsync();

        // Assert
        await CheckInDbAsync(async (db) =>
        {
            var users = await db.Users
                .Include(u => u.Group)
                .Include(u => u.MembershipStatus)
                .ToListAsync();

            var currentUser = users.First(x => x.Id == currentUserModel.UserId);
            var invitedUser = users.Last(x => x.Id == invitedUserModel.UserId);

            currentUser.Group.Should().NotBeNull();
            currentUser.MembershipStatus!.Status.Should().Be(AcceptanceStatus.Completed);
            currentUser.MembershipStatus!.IsInviter.Should().BeNull();

            invitedUser.Group.Should().NotBeNull();
            invitedUser.MembershipStatus!.Status.Should().Be(AcceptanceStatus.Completed);
            invitedUser.MembershipStatus!.IsInviter.Should().BeNull();
        });
    }
}