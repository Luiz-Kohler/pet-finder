using Domain.Enums;

namespace Application.Services.Pet.GetMany
{
    public class GetManyPetsResponse
    {
        public List<PetResponse> Pets { get; set; }
    }

    public class PetResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public EPetType Type { get; set; }
        public EPetSize Size { get; set; }
        public int OldOwnerId { get; set; }
        public virtual UserForPetResponse OldOwner { get; set; }
        public int? NewOwnerId { get; set; }
        public virtual UserForPetResponse NewOwner { get; set; }
        public DateTime? AdoptDate { get; set; }
        public virtual ICollection<ImageForPetResponse> Images { get; set; }
    }

    public class UserForPetResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressForPetResponse Address  { get; set; }
    }

    public class ImageForPetResponse
    {
        public string Url { get; set; }
    }

    public class AddressForPetResponse
    {
        public string State { get; set; }
        public string City { get; set; }
    }
}
