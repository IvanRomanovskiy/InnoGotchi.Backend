namespace InnoGotchi.Application.Common.Exeptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string name, string key)
            : base($"Entity \"{name}\" ({key}) already exists.") { }
    }
}
