using SmokeZeroDigitalSolution.Application.Features.Chat.Commands;
using SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces;

namespace SmokeZeroDigitalSolution.Application.Features.Chat.Validators
{
    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator(IConversationRepository conversationRepository)
        {
            RuleFor(x => x.Message)
                .NotNull().WithMessage("Message data is required.");

            RuleFor(x => x.Message.Content)
                .NotEmpty().WithMessage("Message content must not be empty.");

            RuleFor(x => x)
                .MustAsync(async (cmd, cancellationToken) =>
                {
                    var dto = cmd.Message;
                    var conversation = await conversationRepository.GetByIdWithParticipantsAsync(dto.ConversationId, cancellationToken);

                    if (conversation == null) return false;

                    if (dto.CoachId.HasValue)
                    {
                        var isCoachMatch = conversation.CoachId == dto.CoachId &&
                                           conversation.Coach?.User?.Id == dto.SenderUserId;

                        return isCoachMatch;
                    }

                    return conversation.UserId == dto.SenderUserId;
                })
                .WithMessage("Sender is not a valid participant in the conversation.");
        }
    }
}
