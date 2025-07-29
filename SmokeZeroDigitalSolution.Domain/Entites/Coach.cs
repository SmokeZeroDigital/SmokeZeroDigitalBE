namespace SmokeZeroDigitalSolution.Domain.Entites
{
    public class Coach : BaseEntity
    {
        public Guid UserId { get; set; } // Khóa ngoại tới AppUser
        public AppUser User { get; set; } = default!;

        public string Bio { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsActive { get; set; } = true;

        public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
