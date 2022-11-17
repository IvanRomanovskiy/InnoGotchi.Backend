namespace InnoGotchi.Domain
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PetAppearance Appearance { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfDeath { get; set; }
        public PetStatus Status { get; set; }
        public bool IsAlive { get; set; }


        public void Update()
        {
            double HungryHours;
            double ThirstyHours;

            if (!IsAlive) return;

            if (Status.LastUpdating < Status.LastFeeding)
                HungryHours = (DateTime.Now - Status.LastFeeding).TotalHours;
            else
                HungryHours = ((DateTime.Now - Status.LastFeeding) - (Status.LastUpdating - Status.LastFeeding)).TotalHours;

            if (Status.LastUpdating < Status.LastDrinking)
                ThirstyHours = (DateTime.Now - Status.LastDrinking).TotalHours;
            else
                ThirstyHours = ((DateTime.Now - Status.LastDrinking) - (Status.LastUpdating - Status.LastDrinking)).TotalHours;

            var HungerLevel = Status.HungerLevel;
            var ThirstyLevel = Status.ThirstyLevel;


            Status.HungerLevel = Math.Clamp(HungerLevel - HungryHours * PetStatus.Modifier, PetStatus.MinLevel, PetStatus.MaxLevel);
            Status.ThirstyLevel = Math.Clamp(ThirstyLevel - ThirstyHours * PetStatus.Modifier, PetStatus.MinLevel, PetStatus.MaxLevel);

            if (Status.HungerLevel == 0 || Status.ThirstyLevel == 0)
            {
                var HungerDeathHours = Math.Abs(HungerLevel / PetStatus.Modifier - HungryHours);
                var ThirstyDeathHours = Math.Abs(ThirstyLevel / PetStatus.Modifier - ThirstyHours);

                DateOfDeath = DateTime.Now - TimeSpan.FromHours((HungerDeathHours > ThirstyDeathHours) ? ThirstyDeathHours : HungerDeathHours);
                IsAlive = false;
                Status.LastUpdating = DateTime.Now;
                return;
            }

            if (Status.HungerLevel < 40 || Status.ThirstyLevel < 40)
            {
                Status.HappinessDayCount = 0;
                Status.HappinessDayStart = DateTime.Now;
            }
            else
                Status.HappinessDayCount = (uint)(DateTime.Now - Status.HappinessDayStart).TotalDays;

            Status.Age = (uint)((DateTime.Now - DateOfBirth).TotalDays / 7);

            Status.LastUpdating = DateTime.Now;
        }

        public void Feed()
        {
            if (!IsAlive) return;

            if (Status.HungerLevel >= PetStatus.MaxLevel - 20) return;

            if (Status.HungerLevel < 40)
            {
                Status.HappinessDayCount = 0;
                Status.HappinessDayStart = DateTime.Now;
            }

            Status.HungerLevel = PetStatus.MaxLevel;
            Status.FeedingCount++;
            Status.LastFeeding = DateTime.Now;
        }
        public void Drink()
        {
            if (!IsAlive) return;

            if (Status.ThirstyLevel >= PetStatus.MaxLevel - 20) return;

            if (Status.ThirstyLevel < 40)
            {
                Status.HappinessDayCount = 0;
                Status.HappinessDayStart = DateTime.Now;
            }


            Status.ThirstyLevel = PetStatus.MaxLevel;
            Status.ThirstQuenchingCount++;
            Status.LastDrinking = DateTime.Now;
        }



    }
}