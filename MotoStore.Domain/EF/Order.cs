using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoStore.Domain.EF
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MotoId { get; set; }
        public int UserId { get; set; }
        public int ShopId { get; set; }
        public bool Status { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }

        [ForeignKey(nameof(MotoId))]
        public virtual Motorcycle Motorcycle { get; set; }
        [ForeignKey(nameof(ShopId))]
        public virtual ShopInformation ShopInformation { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}