namespace BookStore.Domain.ViewModels
{
    public class AccountOrderInformationVm
    {
        public ShopInformationVm ShopInformation { get; set; }
        public BookVm Book { get; set; }
        public OrderAccountInfoVm OrderInformation { get; set; }
    }
}
