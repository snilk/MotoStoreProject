using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BookStore.Domain.EF;
using BookStore.Domain.ViewModels;

namespace BookStore.Domain.DataManipulations
{
    public static class BookOperations
    {
        private const string AllLabel = "All";

        public static List<BookVm> GetBooksBySection(string make)
        {
            using (var context = new BookStoreContext())
            {
                if (string.Equals(AllLabel, make, StringComparison.OrdinalIgnoreCase))
                {
                    return context.Books.ToList().Select(book => new BookVm(book)).ToList();
                }

                var books = context.Books.Where(book =>
                    book.Section.Equals(make, StringComparison.OrdinalIgnoreCase));
                
                return books.ToList().Select(book => new BookVm(book)).ToList();
            }
        }

        public static BookVm GetBookById(int id)
        {
            using (var context = new BookStoreContext())
            {
                return context.Books.Where(book => book.Id == id).ToList().Select(book => new BookVm(book))
                    .FirstOrDefault();
            }
        }

        public static List<UniqSectionsVm> GetUniqSections()
        {
            List<UniqSectionsVm> sections;

            using (var context = new BookStoreContext())
            {
                sections = context.Books.Select(c => new UniqSectionsVm {Section = c.Section}).Distinct().ToList();
            }

            return sections;
        }

        public static List<string> GetUniqLevels()
        {
            using (var context = new BookStoreContext())
            {
                return context.Books.Select(c => c.Level).Distinct().ToList();
            }
        }

        public static List<string> GetUniqAuthors()
        {
            using (var context = new BookStoreContext())
            {
                return context.Books.Select(c => c.AuthorName).Distinct().ToList();
            }
        }

        public static Book CreateNewBook(BookVm bookVm, string imagesPath)
        {
            var newBook = new Book
            {
                Title = bookVm.Title,
                Description = bookVm.Description,
                AuthorName = bookVm.AuthorName,
                Section = bookVm.Section,
                ModelsCount = bookVm.ModelsCount,
                Price = bookVm.Price,
                Level = bookVm.Level,
                Year = bookVm.Year
            };

            var mainImage = ImagesOperations.CreateBookImage(bookVm.MainImageFile, imagesPath);
            List<BookImage> additionalImages;

            if (bookVm.AdditionalImagesFiles != null)
            {
                additionalImages = bookVm.AdditionalImagesFiles.Select(vmAdditionalImage =>
                    ImagesOperations.CreateBookImage(vmAdditionalImage, imagesPath)).ToList();
            }
            else
            {
                additionalImages = new List<BookImage>
                {
                    ImagesOperations.CreateBookImage()
                };
            }

            newBook.MainImage = mainImage;
            newBook.BookImages = additionalImages;

            return newBook;
        }

        internal static Book CreateNewBook(BookVm bookVm, FileInfo mainImageInfo, IEnumerable<FileInfo> additionalImagesInfo, string imagesPath)
        {
            var newBook = new Book
            {
                Title = bookVm.Title,
                Description = bookVm.Description,
                AuthorName = bookVm.AuthorName,
                Section = bookVm.Section,
                ModelsCount = bookVm.ModelsCount,
                Price = bookVm.Price,
                Level = bookVm.Level,
                Year = bookVm.Year
            };

            var mainImage = ImagesOperations.CreateBookImage(mainImageInfo, imagesPath);
            List<BookImage> additionalImages;

            if (additionalImagesInfo != null)
            {
                additionalImages = additionalImagesInfo.Select(additionalImage =>
                    ImagesOperations.CreateBookImage(additionalImage, imagesPath)).ToList();
            }
            else
            {
                additionalImages = new List<BookImage>
                {
                    ImagesOperations.CreateBookImage()
                };
            }

            newBook.MainImage = mainImage;
            newBook.BookImages = additionalImages;

            return newBook;
        }
    }
}