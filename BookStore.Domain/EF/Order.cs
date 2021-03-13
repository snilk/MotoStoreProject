using System;
using System.ComponentModel.DataAnnotations.Schema;
using BookStore.Domain.Interfaces;

namespace BookStore.Domain.EF
{
    public class Order : IBookContained
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int ShopId { get; set; }
        public bool Status { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; }
        [ForeignKey(nameof(ShopId))]
        public virtual ShopInformation ShopInformation { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}