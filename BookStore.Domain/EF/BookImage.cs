using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.EF
{
    public class BookImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? BookId { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; }
    }
}