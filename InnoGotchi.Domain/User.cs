namespace InnoGotchi.Domain
{
    public class User
    {
        public Guid Id{ get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string Email{ get; set; }
        public string Password{ get; set; }
        public byte[] Avatar { get; set; }    
        public string Role { get; set; }

        public virtual ICollection<Collaboration> Collaborations { get; set; }
    }
}
