using System.Collections.Generic;

namespace MotoStore.Domain.ViewModels
{
    public class AccountInformationVm
    {
        public OrderUserVm User { get; set; }

        public List<AccountOrderInformationVm> AccountOrdersInformation { get; set; }
    }
}
