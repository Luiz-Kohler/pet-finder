using MediatR;

namespace Application.Services.Pet.Adopt
{
    public class AdoptRequest : IRequest<AdoptResponse>
    {
        public int NewOwnerId { get; set; }
        public int PetId { get; set; }
    }
}
