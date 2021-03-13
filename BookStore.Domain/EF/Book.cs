using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.EF
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Section { get; set; }
        public string Level { get; set; }
        public int Year { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ModelsCount { get; set; }
        public int Price { get; set; }
        public int MainImageId { get; set; }

        [ForeignKey(nameof(MainImageId))]
        public virtual BookImage MainImage { get; set; }
        public virtual ICollection<BookImage> BookImages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Survey> Surveys { get; set; }
    }
}