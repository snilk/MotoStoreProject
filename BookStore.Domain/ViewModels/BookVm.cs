using System.Linq;
using System.Web;
using BookStore.Domain.EF;

namespace BookStore.Domain.ViewModels
{
    public class BookVm
    {
        public BookVm()
        {
        }
        public BookVm(Book book)
        {
            Id = book.Id;
            Section = book.Section;
            Level = book.Level;
            Year = book.Year;
            AuthorName = book.AuthorName;
            Title = book.Title;
            Description = book.Description;
            ModelsCount = book.ModelsCount;
            Price = book.Price;
            MainImage = book.MainImage?.ImageUrl;
            AdditionalImages = book.BookImages?.ToList().Select(bookImage => bookImage.ImageUrl).ToArray();
        }
        public int? Id { get; set; }
        public string Section { get; set; }
        public string Level { get; set; }
        public int Year { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ModelsCount { get; set; }
        public double Price { get; set; }
        public string MainImage { get; set; }
        public string[] AdditionalImages { get; set; }
        public HttpPostedFileBase[] AdditionalImagesFiles { get; set; }
        public HttpPostedFileBase MainImageFile { get; set; }
    }
}
