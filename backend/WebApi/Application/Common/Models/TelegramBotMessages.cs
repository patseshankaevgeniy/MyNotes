namespace Application.Common.Models;

public sealed class TelegramBotMessages
{
    public string CantHandleMessage => "Сорян, но у нас не получается понять ваше сообщение";
    public string UserNoteAdded => "Заметка добавлена";
    public string MyUserNoteAdded => "Вы: {0} {1}";
    public string MemberUserNoteAdded => "Участник добавил заметку";
    public string ServerError => "Произошла ошибка!";
    public string UserNotAuthenticated => "К сожалению вас еще нет в системе.\nПожалуйста введите код что бы авторизироваться !";
    public string WelcomeNewTelegramUser => "Приветы {0}";
    public string WrongAuthCode => "Хреновый код. Давай по новой!";
  
}