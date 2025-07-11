using SmokeZeroDigitalSolution.Application.Features.NotificationManager.DTOs;

namespace SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface
{
    public interface INotiRepo : IBaseRepository<Notification, Guid>
    {
        Task<Notification> CreateNotiAsync(CreateNotiDto dto);
        Task<Notification> UpdateNotiAsync(CreateNotiDto dto);
        Task<IEnumerable<Notification>> GetPendingNotificationsAsync();
        Task<IEnumerable<Notification>> GetNotiByUser(Guid id);
    }
}
