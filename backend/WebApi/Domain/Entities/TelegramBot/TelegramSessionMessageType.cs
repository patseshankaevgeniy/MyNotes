namespace Domain.Entities.NewFolder;

public enum TelegramSessionMessageType
{
    None,
    TextMessageToUser,
    TextMessageWithCallbackToUser,
    TextMessageFromUser,
    CallBackMessageFromUser
}