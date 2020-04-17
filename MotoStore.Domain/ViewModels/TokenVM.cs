namespace BookStore.Domain.ViewModels
{
    public class TokenVm
    {
        public bool? IsAdmin { get; set; }
        public bool IsCorrectToken { get; set; }
    }
}