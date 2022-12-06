namespace InnoGotchi.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(InnoGotchiDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
