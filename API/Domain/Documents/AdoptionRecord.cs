using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Documents
{
    public class AdoptionRecord : BaseEntity
    {
        public Owner OldOwner { get; set; }
        public Owner NewOwner { get; set; }
        public Pet Pet { get; set; }
    }


    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public class OwnerAddress
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
    }

    public class AdoptedPet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public EPetType Type { get; set; }
        public EPetSize Size { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }

}
