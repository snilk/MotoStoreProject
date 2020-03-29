namespace MotoStore.Domain.ViewModels
{
    public class UserTokenRole
    {
        public bool? IsAdmin { get; set; }
        public string token { get; set; }
        public bool? correctUsername { get; set; }
        public bool? correctPassword { get; set; }
    }
}