using MediatR;

namespace Application.Services.Pet.Delete
{
    public class DeletePetRequest : IRequest<DeletePetResponse>
    {
        public int OwnerId { get; set; }
        public int Id { get; set; }
    }
}
