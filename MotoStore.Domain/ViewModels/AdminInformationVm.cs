using System.Collections.Generic;

namespace MotoStore.Domain.ViewModels
{
    public class AdminInformationVm
    {
        public List<OrderInfoAdminVm> orders { get; set; }
        public List<MotorcycleVm> motos { get; set; }
        public List<ShopInformationVm> shopInformations { get; set; }

    }
}