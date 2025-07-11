using SmokeZeroDigitalSolution.Application.Features.NotificationManager.DTOs;
using SmokeZeroDigitalSolution.Application.Features.NotificationManager.Interface;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class NotiService(INotiRepo notiRepo, IEmailService emailService, IUnitOfWork unitOfWork) : INotiService
    {
        private readonly INotiRepo _notiRepo = notiRepo;
        private readonly IEmailService _emailService = emailService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Notification> CreateNotiAsync(CreateNotiDto dto)
        {
            return await _notiRepo.CreateNotiAsync(dto);
        }

        public async Task<bool> DeleteNotificationAsync(Guid id)
        {
           return await _notiRepo.Remove(id);
        }

        public async Task<IQueryable<Notification>> GetAllAsync()
        {
            return _notiRepo.GetAll()
                .Include(n => n.User);
        }

        public Task<Notification> GetNotificationByIdAsync(Guid id)
        {
            return _notiRepo.GetAll()
                .Include(n => n.User)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Notification> UpdateNotiAsync(CreateNotiDto dto)
        {
            return await _notiRepo.UpdateNotiAsync(dto);
        }

        public async Task SendPendingNotificationsAsync()
        {
            var notifications = await _notiRepo.GetPendingNotificationsAsync();
            if (notifications == null || !notifications.Any())
            {
                Console.WriteLine(DateTime.UtcNow);
                return;
            }
            foreach (var noti in notifications)
            {
                try
                {
                    await _emailService.SendAsync(noti.User.Email, noti.Title, noti.Message);

                    noti.SentTime = DateTime.UtcNow;
                    noti.ScheduledTime = noti.ScheduledTime.AddDays(noti.RecurrencePattern);
                    Console.WriteLine(noti.RecurrencePattern);
                    Console.WriteLine(noti.ScheduledTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            _unitOfWork.SaveAsync();

        }

        public async Task<IEnumerable<Notification>> GetNotiByUid(Guid id)
        {
            return await _notiRepo.GetNotiByUser(id); 
        }
    }
}
