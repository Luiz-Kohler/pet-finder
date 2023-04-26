using Domain.Common;

namespace Domain.Entities
{
    public class Address : BaseEntity
    {
        public string State { get; set; }
        public string City { get; set; }
        public virtual User User { get; set; }
    }
}
