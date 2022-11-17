using System.Text.Json.Serialization;

namespace InnoGotchi.Domain
{
    public class PetStatus
    {
        [JsonIgnore]
        public const double Modifier = 2.5;
        [JsonIgnore]
        public const double MinLevel = 0;
        [JsonIgnore]
        public const double MaxLevel = 100;

        [JsonIgnore]
        public Guid Id { get; set; }

        public uint Age { get; set; }
        public double HungerLevel { get; set; }
        public int FeedingCount { get; set; }
        public double ThirstyLevel { get; set; }
        public int ThirstQuenchingCount { get; set; }
        public uint HappinessDayCount { get; set; }




        [JsonIgnore]
        public DateTime HappinessDayStart { get; set; }
        [JsonIgnore]
        public DateTime LastFeeding { get; set; }
        [JsonIgnore]
        public DateTime LastDrinking { get; set; }
        [JsonIgnore]
        public DateTime LastUpdating { get; set; }
    }
}