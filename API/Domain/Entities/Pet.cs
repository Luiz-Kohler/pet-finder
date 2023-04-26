using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Pet : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public EPetType Type { get; set; }
        public EPetSize Size { get; set; }
        public int OldOwnerId { get; set; }
        public virtual User OldOwner { get; set; }
        public int? NewOwnerId { get; set; }
        public virtual User NewOwner { get; set; }
        public DateTime? AdoptDate { get; set; }     
        public virtual ICollection<Image> Images { get; set; }
    }
}
