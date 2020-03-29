﻿using System.Collections.Generic;

namespace MotoStore.Domain.EF
{
    public class Motorcycle
    {
        public int Id { get; set; }
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

        public virtual ICollection<MotoImages> MotoImages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}