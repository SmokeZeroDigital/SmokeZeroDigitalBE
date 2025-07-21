using SmokeZeroDigitalSolution.Application.Features.NotificationManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface
{
    public interface INotiService
    {
        Task<Notification> CreateNotiAsync(CreateNotiDto dto);
        Task<IQueryable<Notification>> GetAllAsync();
        Task<IEnumerable<Notification>> GetNotiByUid(Guid id);
        Task<Notification> GetNotificationByIdAsync(Guid id);
        Task<bool> DeleteNotificationAsync(Guid id);
        Task<Notification> UpdateNotiAsync(CreateNotiDto dto);
        Task SendPendingNotificationsAsync();
    }
}
