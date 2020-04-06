using System;
using System.IO;
using System.Web;
using MotoStore.Domain.EF;
using MotoStore.Domain.Static;

namespace MotoStore.Domain.DataManipulations
{
    public class ImagesOperations
    {
        public static MotoImage CreateMotoImage(HttpPostedFileBase image = null, string imagesPath = null)
        {
            var imageFileName = MotoImagesConstants.PlaceHolderImageUrl;

            if (image != null && imagesPath != null)
            {
                var extenstion = Path.GetExtension(image.FileName);
                var imageName = Guid.NewGuid().ToString();
                imageFileName = imageName + extenstion;

                image.SaveAs(Path.Combine(imagesPath, imageFileName));
            }

            return new MotoImage
            {
                ImageUrl = imageFileName
            };
        }
    }
}
