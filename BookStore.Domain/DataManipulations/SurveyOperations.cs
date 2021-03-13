using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Domain.EF;
using BookStore.Domain.ViewModels;
using BookStore.Domain.ViewModels.Survey;

namespace BookStore.Domain.DataManipulations
{
    public class SurveyOperations
    {
        public static SuccessVm SubmitSurvey(SubmitSurveyVm submitSurveyVm)
        {
            var successVm = new SuccessVm();

            if (submitSurveyVm != null)
            {
                using (var context = new BookStoreContext())
                {
                    var user = UsersOperations.GetUserByToken(submitSurveyVm.UserToken, context);

                    if (user == null)
                    {
                        successVm.SystemDescription = "Incorrect token";

                        return successVm;
                    }

                    var book = context.Books.FirstOrDefault(bookItem => bookItem.Id == submitSurveyVm.BookId);

                    if (book == null)
                    {
                        successVm.SystemDescription = "Incorrect book id";

                        return successVm;
                    }

                    successVm.Success = true;

                    var survey = new Survey
                    {
                        Book = book,
                        User = user,
                        Goal = submitSurveyVm.Goal,
                        Rate = submitSurveyVm.Rate,
                    };

                    context.Surveys.Add(survey);
                    context.SaveChanges();
                }
            }

            return successVm;
        }

        public static AverageRateVm GetAverageRate()
        {
            var averageRateVm = new AverageRateVm();

            using (var context = new BookStoreContext())
            {
                var surveys = context.Surveys;

                if (surveys.Any())
                {
                    averageRateVm.AverageRate = surveys.Average(survey => survey.Rate);
                }
            }

            return averageRateVm;
        }

        public static IList<PopularBookVm> GetPopularBooks(string goal)
        {
            using (var context = new BookStoreContext())
            {
                return AdminOperations.GetPopularBooks(context.Surveys, survey => SurveyByGoalPredicate(survey, goal));
            }
        }

        public static IList<PopularBookVm> GetPopularBooks(string department, string goal)
        {
            using (var context = new BookStoreContext())
            {
                return AdminOperations.GetPopularBooks(context.Surveys, survey =>
                    SurveyByGoalPredicate(survey, goal) && survey.User != null &&
                    string.Equals(survey.User.Department, department, StringComparison.OrdinalIgnoreCase));
            }
        }

        private static bool SurveyByGoalPredicate(Survey survey, string goal)
        {
            return string.Equals(survey.Goal, goal, StringComparison.OrdinalIgnoreCase);
        }
    }
}