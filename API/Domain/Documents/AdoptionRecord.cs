using Domain.Common;
using Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Documents
{
    public class AdoptionRecord 
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Owner OldOwner { get; set; }
        public Owner NewOwner { get; set; }
        public AdoptedPet Pet { get; set; }
    }


    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public OwnerAddress Address { get; set; }
    }

    public class OwnerAddress
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }
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
