namespace BookStore.Domain.ViewModels
{
    public class UserTokenRole
    {
        public bool? IsAdmin { get; set; }
        public string Token { get; set; }
        public bool? CorrectUsername { get; set; }
        public bool? CorrectPassword { get; set; }
    }
}