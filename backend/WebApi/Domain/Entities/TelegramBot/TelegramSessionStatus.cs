namespace Domain.Entities.NewFolder;

public enum TelegramSessionStatus
{
    None,
    WaitForTextMessage,
    WaitForCallBack,
    WaitForTextMessageOrCallback,
    Completed
}