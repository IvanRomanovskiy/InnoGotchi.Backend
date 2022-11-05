namespace InnoGotchi.Domain
{
    public class PetStatus
    {
        public Guid Id { get; set; }
        public uint Age { get; set; }
        public int HungerLevel { get; set; }
        public int FeedingCount { get; set; }
        public int ThirstyLevel { get; set; }
        public int ThirstQuenchingCount { get; set; }
        public uint HappinessDayCount { get; set; }
        public DateTime LastFeeding { get; set; }
        public DateTime LastDrinking { get; set; }
    }
}