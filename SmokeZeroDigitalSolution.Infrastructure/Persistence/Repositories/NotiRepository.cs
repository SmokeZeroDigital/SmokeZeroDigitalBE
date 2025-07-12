using SmokeZeroDigitalSolution.Application.Features.NotificationManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories
{
    public class NotiRepository(ApplicationDbContext applicationDbContext) : BaseRepository<Notification, Guid>(applicationDbContext),INotiRepo
    {
        public async Task<Notification> CreateNotiAsync(CreateNotiDto dto)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                Type = dto.Type,
                Title = dto.Title,
                Message = dto.Message,
                ScheduledTime = dto.ScheduledTime,
                RecurrencePattern = dto.RecurrencePattern,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };
            await AddAsync(notification);
            return notification;
        }

        public async Task<IEnumerable<Notification>> GetNotiByUser(Guid id)
        {
            return await Get(
            n => n.UserId == id)
                .Include(n => n.User)
        .OrderBy(n => n.ScheduledTime)
        .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetPendingNotificationsAsync()
        {
            return await Get(
            n => n.ScheduledTime <= DateTime.UtcNow)
                .Include(n => n.User)
        .OrderBy(n => n.ScheduledTime)
        .ToListAsync();
        }

        public async Task<Notification> UpdateNotiAsync(CreateNotiDto dto)
        {
            var notification = await FindAsync(dto.UserId);
            if (notification == null) throw new KeyNotFoundException("Notification not found.");
            notification.Type = dto.Type;
            notification.Title = dto.Title;
            notification.RecurrencePattern = dto.RecurrencePattern;
            notification.Message = dto.Message;
            notification.ScheduledTime = dto.ScheduledTime;
            notification.LastModifiedAt = DateTime.UtcNow;
            Update(notification);
            return notification;
        }
    }
}
