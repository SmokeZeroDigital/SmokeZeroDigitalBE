//using AutoMapper;
//using Microsoft.AspNetCore.SignalR;
//using SmokeZeroDigitalSolution.Application.Features.Chat.DTOs;
//using SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces;

//namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
//{
//    public class ChatService : IChatService
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly IHubContext<ChatHub> _hub;
//        private readonly IMapper _mapper;

//        public ChatService(ApplicationDbContext context, IHubContext<ChatHub> hub, IMapper mapper)
//        {
//            _context = context;
//            _hub = hub;
//            _mapper = mapper;
//        }

//        public async Task<ChatMessageDto> SendMessageAsync(SendMessageDto dto, CancellationToken ct)
//        {
//            var message = new ChatMessage
//            {
//                Id = Guid.NewGuid(),
//                ConversationId = dto.ConversationId,
//                SenderUserId = dto.SenderUserId,
//                CoachId = dto.CoachId,
//                Content = dto.Content,
//                MessageType = dto.MessageType ?? "TEXT",
//                Timestamp = DateTime.UtcNow,
//                CreatedAt = DateTime.UtcNow
//            };

//            _context.ChatMessages.Add(message);

//            // Cập nhật nội dung cuối
//            var conversation = await _context.Conversations.FindAsync(new object[] { dto.ConversationId }, ct);
//            if (conversation != null)
//            {
//                conversation.LastMessage = dto.Content;
//                conversation.LastMessageSender = dto.CoachId.HasValue ? "Coach" : "User";
//            }

//            await _context.SaveChangesAsync(ct);

//            var result = _mapper.Map<ChatMessageDto>(message);

//            // Gửi realtime tới group của conversation
//            await _hub.Clients.Group(dto.ConversationId.ToString()).SendAsync("ReceiveMessage", result, ct);

//            return result;
//        }
//    }
//}
