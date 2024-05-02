using Domain.Entities;
using FluentAssertions;
using IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests.Members;

public sealed class CreateMemberTests : IntegrationTest
{
    [Fact]
    public async Task CreateMember_ShouldInviteToGroup_WithCorrectStatuses_Async()
    {
        // Arrange
        var currentUserModel = await AuthContext.CreateUserAndAuthenticateAsync();
        var invitedUserModel = await AuthContext.CreateUserAsync();

        // Act
        await MynotesClient.CreateMemberAsync(new() { UserId = invitedUserModel.UserId });

        // Assert
        await CheckInDbAsync(async db =>
        {
            var users = await db.Users
                .Include(u => u.Group)
                .Include(u => u.MembershipStatus)
                .ToListAsync();

            var currentUser = users.First(x => x.Id == currentUserModel.UserId);
            var invitedUser = users.Last(x => x.Id == invitedUserModel.UserId);

            currentUser.Group.Should().NotBeNull();
            currentUser.MembershipStatus!.Status.Should().Be(AcceptanceStatus.Pending);
            currentUser.MembershipStatus!.IsInviter.Should().BeTrue();

            invitedUser.Group.Should().NotBeNull();
            invitedUser.MembershipStatus!.Status.Should().Be(AcceptanceStatus.Pending);
            invitedUser.MembershipStatus!.IsInviter.Should().BeFalse();
        });
    }
}