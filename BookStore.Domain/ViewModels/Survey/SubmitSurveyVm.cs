namespace BookStore.Domain.ViewModels.Survey
{
    public class SubmitSurveyVm
    {
        public string UserToken { get; set; }

        public int BookId { get; set; }

        public string Goal { get; set; }

        public int Rate { get; set; }
    }
}