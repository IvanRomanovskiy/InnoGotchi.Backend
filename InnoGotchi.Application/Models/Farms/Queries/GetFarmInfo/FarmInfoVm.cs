namespace InnoGotchi.Application.Models.Farms.Queries.GetFarmInfo
{
    public class FarmInfoVm
    {
        public string Name { get; set; }
        public int CountAlivePets { get; set; }
        public int CountDeadPets { get; set; }
        public TimeSpan AverageFeedingPeriod { get; set; }
        public TimeSpan AverageThirstQuenchingPeriod { get; set; }
        public long AveragePetsHappinessDaysCount { get; set; }
        public long AveragePetsAgeCount { get; set; }
    }
}