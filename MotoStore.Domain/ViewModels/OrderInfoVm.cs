namespace BookStore.Domain.ViewModels
{
    public class OrderInfoVm
    {
        public string Token { get; set; }
        public int BookId { get; set; }
        public int ShopId { get; set; }
        public string Address { get; set; }
    }
}