using System.ComponentModel.DataAnnotations.Schema;
using BookStore.Domain.Interfaces;

namespace BookStore.Domain.EF
{
    public class Survey : IBookContained
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public string Goal { get; set; }

        public int Rate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; }
    }
}