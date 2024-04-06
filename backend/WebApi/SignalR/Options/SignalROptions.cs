namespace SignalR.Options;

public sealed class SignalROptions
{
    public const string SectionName = "SignalR";

    public string FrontendUrl { get; init; } = default!;
    public string NotificationMethodName { get; init; } = default!;
}