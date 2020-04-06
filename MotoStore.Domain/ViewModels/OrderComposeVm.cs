using System.Collections.Generic;

namespace MotoStore.Domain.ViewModels
{
    public class OrderComposeVm
    {
        public OrderUserVm User { get; set; }
        public List<ShopInformationVm> ShopsInformation { get; set; }
    }
}
