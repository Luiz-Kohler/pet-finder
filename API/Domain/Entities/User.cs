using Domain.Common;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Pet> PetsToAdopt { get; set; }
        public virtual ICollection<Pet> PetsAdopted { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
