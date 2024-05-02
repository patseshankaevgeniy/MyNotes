using Domain.Entities;
using FluentAssertions;
using IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests.Members;

public sealed class DeleteMemberTests : IntegrationTest
{
    [Fact]
    public async Task ProposeMembership_ShouldDeleteMember_Async()
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
        var userMembers = await MynotesClient.GetMembersAsync();
        await MynotesClient.DeleteMemberAsync(userMembers.First().Id);

        // Assert
        await CheckInDbAsync(async (db) =>
        {
            var users = await db.Users
                .Include(u => u.Group)
                .Include(u => u.MembershipStatus)
                .ToListAsync();

            foreach (var user in users)
            {
                user.GroupId.Should().BeNull();
                user.MembershipStatus.Should().BeNull();
            }
        });
    }
}
