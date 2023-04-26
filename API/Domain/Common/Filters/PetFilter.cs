using Domain.Enums;

namespace Domain.Common.Filter
{
    public class PetFilter
    {
        public int? Id { get; set; }
        public EPetType? Type { get; set; }
        public EPetSize? Size { get; set; }
        public bool? WasAlreadyAdopted { get; set; }
        public int? NewOwnerId { get; set; }
        public int? OldOwnerId { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
    }
}
