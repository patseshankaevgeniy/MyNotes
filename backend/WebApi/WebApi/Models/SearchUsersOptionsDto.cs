namespace WebApi.Models;

public sealed class SearchUsersOptionsDto
{
    public bool? HasMembers { get; init; }
    public bool? ExcludeCurrent { get; init; }
    public string SearchPattern { get; init; }
    public bool? OnlyCurrentUserMembers { get; init; }
}