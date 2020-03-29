using System.Collections.Generic;

namespace MotoStore.Domain.EF
{
    public class ShopInformation
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}