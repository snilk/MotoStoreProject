namespace BookStore.Domain.ViewModels
{
    public class SuccessVm
    {
        public SuccessVm()
        {
        }

        public SuccessVm(bool success)
        {
            Success = success;
        }

        public bool Success { get; set; }
    }
}