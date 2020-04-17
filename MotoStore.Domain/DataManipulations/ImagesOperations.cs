using System;
using System.IO;
using System.Web;
using BookStore.Domain.EF;
using BookStore.Domain.Static;

namespace BookStore.Domain.DataManipulations
{
    public class ImagesOperations
    {
        public static BookImage CreateBookImage(HttpPostedFileBase image = null, string imagesPath = null)
        {
            var imageFileName = BookImagesConstants.PlaceHolderImageUrl;

            if (image != null && imagesPath != null)
            {
                var extenstion = Path.GetExtension(image.FileName);
                var imageName = Guid.NewGuid().ToString();
                imageFileName = imageName + extenstion;

                image.SaveAs(Path.Combine(imagesPath, imageFileName));
            }

            return new BookImage
            {
                ImageUrl = imageFileName
            };
        }
    }
}
