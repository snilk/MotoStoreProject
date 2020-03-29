using System.Collections;
using System.Collections.Generic;
using MotoStore.Domain.EF;

namespace MotoStore.Domain.ViewModels
{
    public class AdminInformationVm
    {
        public List<OrderInfoAdminVm> orders { get; set; }
        public List<MotorcycleVm> motos { get; set; }
        public List<ShopInformationVm> shopInformations { get; set; }

    }
}