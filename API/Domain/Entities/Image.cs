using Domain.Common;

namespace Domain.Entities
{
    public class Image : BaseEntity
    {
        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }
        public string Url { get; set; }
    }
}
