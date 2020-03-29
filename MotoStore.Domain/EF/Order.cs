using System;

namespace MotoStore.Domain.EF
{
    public class Order
    {
        public int Id { get; set; }
        public int MotoId { get; set; }
        public int UserId { get; set; }
        public int ShopId { get; set; }
        public bool Status { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Motorcycle Motorcycle { get; set; }
        public virtual ShopInformation ShopInformation { get; set; }
        public virtual User User { get; set; }
    }
}