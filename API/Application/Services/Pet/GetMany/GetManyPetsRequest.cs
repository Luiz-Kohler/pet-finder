using Domain.Enums;
using MediatR;

namespace Application.Services.Pet.GetMany
{
    public class GetManyPetsRequest : IRequest<GetManyPetsResponse>
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
