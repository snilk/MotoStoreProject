using System.Linq;
using System.Web;
using MotoStore.Domain.EF;

namespace MotoStore.Domain.ViewModels
{
    public class MotorcycleVm
    {
        public MotorcycleVm()
        {
        }
        public MotorcycleVm(Motorcycle motorcycle)
        {
            Id = motorcycle.Id;
            Make = motorcycle.Make;
            Type = motorcycle.Type;
            Year = motorcycle.Year;
            EngineCapacity = motorcycle.EngineCapacity;
            Cylinders = motorcycle.Cylinders;
            Abs = motorcycle.Abs;
            ElectricStarter = motorcycle.ElectricStarter;
            CruizeControl = motorcycle.CruizeControl;
            Description = motorcycle.Description;
            ModelsCount = motorcycle.ModelsCount;
            Price = motorcycle.Price;
            MainImage = motorcycle.MainImage?.ImageUrl;
            AdditionalImages = motorcycle.MotoImages?.ToList().Select(motoImage => motoImage.ImageUrl).ToArray();
        }
        public int? Id { get; set; }
        public string Make { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public double EngineCapacity { get; set; }
        public int Cylinders { get; set; }
        public bool Abs { get; set; }
        public bool ElectricStarter { get; set; }
        public bool CruizeControl { get; set; }
        public string Description { get; set; }
        public int ModelsCount { get; set; }
        public double Price { get; set; }
        public string MainImage { get; set; }
        public string[] AdditionalImages { get; set; }
        public HttpPostedFileBase[] AdditionalImagesFiles { get; set; }
        public HttpPostedFileBase MainImageFile { get; set; }
    }
}
