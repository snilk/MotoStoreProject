using System;
using System.Collections.Generic;
using System.Linq;
using MotoStore.Domain.EF;
using MotoStore.Domain.ViewModels;

namespace MotoStore.Domain.DataManipulations
{
    public static class MotorcycleOperations
    {
        private const string AllLabel = "All";

        public static List<MotorcycleVm> GetMotorcyclesByMake(string make)
        {
            using (var context = new MotoStoreContext())
            {
                if (string.Equals(AllLabel, make, StringComparison.OrdinalIgnoreCase))
                {
                    return context.Motorcycles.ToList().Select(moto => new MotorcycleVm(moto)).ToList();
                }

                var motorcycles = context.Motorcycles.Where(moto =>
                    moto.Make.Equals(make, StringComparison.OrdinalIgnoreCase));

                return motorcycles.ToList().Select(moto => new MotorcycleVm(moto)).ToList();
            }
        }

        public static MotorcycleVm GetMotoById(int id)
        {
            using (var context = new MotoStoreContext())
            {
                return context.Motorcycles.Where(moto => moto.Id == id).ToList().Select(moto => new MotorcycleVm(moto))
                    .FirstOrDefault();
            }
        }

        public static List<UniqMakesVm> GetUniqCategories()
        {
            List<UniqMakesVm> categories;

            using (var context = new MotoStoreContext())
            {
                categories = context.Motorcycles.Select(c => new UniqMakesVm {Make = c.Make}).Distinct().ToList();
            }

            return categories;
        }

        public static List<string> GetUniqTypes()
        {
            using (var context = new MotoStoreContext())
            {
                return context.Motorcycles.Select(c => c.Type).Distinct().ToList();
            }
        }

        public static Motorcycle CreateNewMotorcycle(MotorcycleVm motorcycleVm, string imagesPath)
        {
            var motorcycle = new Motorcycle
            {
                Abs = motorcycleVm.Abs,
                CruizeControl = motorcycleVm.CruizeControl,
                Cylinders = motorcycleVm.Cylinders,
                Description = motorcycleVm.Description,
                ElectricStarter = motorcycleVm.ElectricStarter,
                EngineCapacity = motorcycleVm.EngineCapacity,
                Make = motorcycleVm.Make,
                ModelsCount = motorcycleVm.ModelsCount,
                Price = motorcycleVm.Price,
                Type = motorcycleVm.Type,
                Year = motorcycleVm.Year
            };

            var mainImage = ImagesOperations.CreateMotoImage(motorcycleVm.MainImageFile, imagesPath);
            List<MotoImage> additionalImages;

            if (motorcycleVm.AdditionalImagesFiles != null)
            {
                additionalImages = motorcycleVm.AdditionalImagesFiles.Select(motorcycleVmAdditionalImage =>
                    ImagesOperations.CreateMotoImage(motorcycleVmAdditionalImage, imagesPath)).ToList();
            }
            else
            {
                additionalImages = new List<MotoImage>
                {
                    ImagesOperations.CreateMotoImage()
                };
            }

            motorcycle.MainImage = mainImage;
            motorcycle.MotoImages = additionalImages;

            return motorcycle;
        }
    }
}