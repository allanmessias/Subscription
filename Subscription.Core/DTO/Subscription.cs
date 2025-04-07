namespace Domain.Domain
{
    public class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public int StatusId { get; set; }
        public required Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public ICollection<EventHistory> EventHistories { get; set; } = new List<EventHistory>();
    }
}
