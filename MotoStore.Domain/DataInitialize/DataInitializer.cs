using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BookStore.Domain.DataManipulations;
using BookStore.Domain.EF;
using BookStore.Domain.Extensions;
using BookStore.Domain.Static;
using BookStore.Domain.ViewModels;

namespace BookStore.Domain.DataInitialize
{
    public static class DataInitializer
    {
        private const string AuthorsFilePath = @"DataInitializeFiles\TextData\Authors.txt";
        private const string TitlesFilePath = @"DataInitializeFiles\TextData\Titles.txt";
        private const string DescriptionFilePath = @"DataInitializeFiles\TextData\Descriptions.txt";
        private const string MainPhotosDirectory = @"DataInitializeFiles\Osnovnoe_Foto";
        private const string AdditionalPhotosDirectory = @"DataInitializeFiles\Dopolnitelnye_Foto";


        private static readonly Random random = new Random();
        public static void InitializeTables()
        {
            using (var context = new BookStoreContext())
            {
                var admin = new User
                {
                    Email = "admin@admin.com",
                    Name = "Administrator",
                    Password = "admin",
                    Phone = "admin_phone",
                    IsAdmin = true,
                    Surname = "Administrator",
                    UserName = "admin",
                    RegistrationDate = DateTime.Now
                };

                if (!context.Users.Any(user => string.Equals(user.UserName, admin.UserName)))
                {
                    context.Users.Add(admin);
                    context.SaveChanges();
                }
            }
        }

        public static void RemoveDuplicates()
        {
            using (var context = new BookStoreContext())
            {
                var query = context.Users.OrderBy(u=>u.Id).GroupBy(x => new { x.UserName, x.Password, x.Name, x.Surname, x.Phone, x.RegistrationDate, x.Email, x.IsAdmin })
                    .SelectMany(x => x.OrderBy(y=>y.Id).Skip(1));
                if (query.Any())
                {
                    context.Users.RemoveRange(query);
                }
                context.SaveChanges();
            }
        }

        public static void InitializeBooks(string projectDirectory, string imagesPath)
        {
            var fullAuthorsFilePath = Path.Combine(projectDirectory, AuthorsFilePath);
            var fullTitlesFilePath = Path.Combine(projectDirectory, TitlesFilePath);
            var fullDescriptionFilePath = Path.Combine(projectDirectory, DescriptionFilePath);
            var fullMainPhotosDirectory = Path.Combine(projectDirectory, MainPhotosDirectory);
            var fullAdditionalPhotosDirectory = Path.Combine(projectDirectory, AdditionalPhotosDirectory);

            var availableSections = EnumExtensions.GetValues<SectionEnum>().Select(val => val.GetEnumDescription())
                .ToList();
            var availableLevels =
                EnumExtensions.GetValues<LevelEnum>().Select(val => val.GetEnumDescription()).ToList();
            var availableAuthors = GetArrayStringFromFile(fullAuthorsFilePath);
            var availableTitles = GetArrayStringFromFile(fullTitlesFilePath);
            var availableDescriptions = GetArrayStringFromFile(fullDescriptionFilePath);
            var availableMainImages = Directory.GetFiles(fullMainPhotosDirectory).Select(img => new FileInfo(img)).ToList();
            var availableAdditionalImages =
                Directory.GetFiles(fullAdditionalPhotosDirectory).Select(img => new FileInfo(img)).ToList();

            var newBooks = new List<Book>();
            foreach (var availableSection in availableSections)
            {
                for (var i = 0; i < 2; i++)
                {
                    foreach (var availableLevel in availableLevels)
                    {
                        var bookVm = new BookVm
                        {
                            Description = availableDescriptions.GetRandomItem(),
                            AuthorName = availableAuthors.GetRandomItem(),
                            Level = availableLevel,
                            ModelsCount = RandomNumberOfInstances,
                            Price = RandomPrice,
                            Section = availableSection,
                            Title = availableTitles.GetRandomItem(),
                            Year = RandomYear
                        };
                        var mainImage = availableMainImages.GetRandomItem();
                        var additionalImages = availableAdditionalImages.GetRandomItemList(RandomAdditionalImagesCount);

                        var book = BookOperations.CreateNewBook(bookVm, mainImage, additionalImages,
                            imagesPath);

                        newBooks.Add(book);
                    }
                }
            }

            using (var context = new BookStoreContext())
            {
                var existingBookIds = context.Books.Select(book => book.Id).ToList();

                foreach (var existingBookId in existingBookIds)
                {
                    AdminOperations.RemoveBookById(existingBookId);
                }

                context.Books.AddRange(newBooks);

                context.SaveChanges();
            }
        }

        private static int RandomYear => random.Next(2010, 2021);
        private static int RandomNumberOfInstances => random.Next(0, 31);
        private static int RandomPrice => random.Next(10, 151);
        private static int RandomAdditionalImagesCount => random.Next(2, 4);

        private static IList<string> GetArrayStringFromFile(string filePath)
        {
            var text = File.ReadAllText(filePath);
            return text.Replace("\n","").Split('\r').Where(x=>!string.IsNullOrEmpty(x)).ToList();
        }
    }
}