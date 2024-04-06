using Application.Common.Models;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface INotificationService
{
    Task SendNotificationAsync(NotificationModel model);
}
