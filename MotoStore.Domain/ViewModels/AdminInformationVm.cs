using System.Collections.Generic;

namespace BookStore.Domain.ViewModels
{
    public class AdminInformationVm
    {
        public List<OrderInfoAdminVm> orders { get; set; }
        public List<BookVm> books { get; set; }
        public List<ShopInformationVm> shopInformations { get; set; }

    }
}