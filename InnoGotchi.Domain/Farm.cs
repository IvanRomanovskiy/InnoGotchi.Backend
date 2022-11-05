
namespace InnoGotchi.Domain
{
    public class Farm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Collaboration> Collaborations { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
