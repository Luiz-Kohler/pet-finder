using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Pet.Create
{
    public class CreatePetRequest : IRequest<CreatePetResponse>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public EPetType Type { get; set; }
        public EPetSize Size { get; set; }
        public int OldOwnerId { get; set; }
        public List<IFormFile>? Images { get; set; }

    }
}
