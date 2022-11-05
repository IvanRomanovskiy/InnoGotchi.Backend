
using InnoGotchi.Domain.Appearance;

namespace InnoGotchi.Domain
{
    public class PetAppearance
    {
        public Guid Id { get; set; }
        public Bodies Body { get; set; }
        public Eyes Eye { get; set; }
        public Mouths Mouth { get; set; }
        public Noses Nose { get; set; }
    }
}
