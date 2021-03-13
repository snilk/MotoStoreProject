using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookStore.Domain.EF;
using BookStore.Domain.Interfaces;
using BookStore.Domain.ViewModels;
using BookStore.Domain.ViewModels.Admin;

namespace BookStore.Domain.DataManipulations
{
    public class AdminOperations
    {
        public static AdminInformationVm GetInfoFormAdmin()
        {
            var context = new BookStoreContext();

            var orderList = OrderOperations.GetAllOrders();

            var shopInformationVms = context.ShopInformations.ToList().Select(info => new ShopInformationVm
            {
                Phone1 = info.Phone1,
                Address = info.Address,
                Phone2 = info.Phone2,
                Id = info.Id
            }).ToList();

            var bookList = context.Books.ToList().Select(book=> new BookVm(book)).ToList();
            var adminInformationVm = new AdminInformationVm
            {
                books = bookList,
                orders = orderList,
                shopInformations = shopInformationVms
            };
            return adminInformationVm;
        }

        public static SuccessVm ChangeOrderStatus(int id)
        {
            using (var context = new BookStoreContext())
            {
                var changedOrder = context.Orders.FirstOrDefault(o => o.Id == id);

                if (changedOrder == null)
                {
                    return new SuccessVm(false)
                    {
                        SystemDescription = "Incorrect order id"
                    };
                }

                var user = changedOrder.User;

                if (user == null)
                {
                    return new SuccessVm(false)
                    {
                        SystemDescription = "There are no user for this order"
                    };
                }

                var book = changedOrder.Book;

                if (!changedOrder.Status && user.BonusPoints < book.Price)
                {
                    return new SuccessVm(false)
                    {
                        Description = "The user doesn't have enough bonus points to get this book"
                    };
                }

                changedOrder.Status = !changedOrder.Status;

                var bonusOffset = changedOrder.Status ? book.Price : -book.Price;
                user.BonusPoints += bonusOffset;

                var modelsCountOffset = changedOrder.Status ? -1 : 1;
                book.ModelsCount += modelsCountOffset;

                context.SaveChanges();
            }

            return new SuccessVm(true);
        }

        public static SuccessVm RemoveBookById(int id)
        {
            using (var context = new BookStoreContext())
            {
                var removedBook = context.Books.FirstOrDefault(m => m.Id == id);

                if (removedBook == null)
                {
                    return new SuccessVm(false);
                }

                context.BookImages.RemoveRange(removedBook.BookImages);
                context.BookImages.Remove(removedBook.MainImage);
                context.Books.Remove(removedBook);
                context.SaveChanges();
            }

            return new SuccessVm(true);
        }

        public static SuccessVm AddNewBook(BookVm bookVm, string imagesPath)
        {
            using (var context = new BookStoreContext())
            {
                context.Books.Add(BookOperations.CreateNewBook(bookVm, imagesPath));
                context.SaveChanges();
            }

            return new SuccessVm(true);
        }

        public static SuccessVm AddShop(ShopInformationVm shopInformationVm)
        {
            using (var context = new BookStoreContext())
            {
                context.ShopInformations.Add(new ShopInformation
                {
                    Address = shopInformationVm.Address,
                    Phone1 = shopInformationVm.Phone1,
                    Phone2 = shopInformationVm.Phone2
                });
                context.SaveChanges();
            }

            return new SuccessVm(true);
        }

        public static SuccessVm RemoveShopById(int id)
        {
            using (var context = new BookStoreContext())
            {
                var shopForRemove = context.ShopInformations.FirstOrDefault(m => m.Id == id);
                if (shopForRemove == null)
                {
                    return new SuccessVm(false);
                }

                context.ShopInformations.Remove(shopForRemove);
                context.SaveChanges();
            }

            return new SuccessVm(true);
        }

        public static AdminUsersInformation GetUsersInformation()
        {
            var adminUsersInformation = new AdminUsersInformation();
            var users = new List<OrderUserVm>();

            using (var context = new BookStoreContext())
            {
                context.Users.ToList().ForEach(user => users.Add(new OrderUserVm(user)));
            }

            adminUsersInformation.Users = users;

            return adminUsersInformation;
        }

        public static SuccessVm ApplyBonusPoints(ApplyBonusPointsVm applyBonusPointsVm)
        {
            var successVm = new SuccessVm();
            using (var context = new BookStoreContext())
            {
                var userToApplyPoints = UsersOperations.GetUserByToken(applyBonusPointsVm.UserToken, context);

                if (userToApplyPoints != null)
                {
                    successVm.Success = true;

                    userToApplyPoints.BonusPoints = applyBonusPointsVm.BonusPoints;
                    context.SaveChanges();
                }
            }

            return successVm;
        }

        public static IList<PopularBookVm> GetPopularBooks(string department)
        {
            using (var context = new BookStoreContext())
            {
                return GetPopularBooks(context.Orders,
                    order => order.User != null && string.Equals(department, order.User.Department, StringComparison.OrdinalIgnoreCase));
            }
        }

        internal static IList<PopularBookVm> GetPopularBooks<T>(IQueryable<T> dbSet, Expression<Func<T, bool>> predicate) where T : class, IBookContained
        {
            var popularBookVms = new List<PopularBookVm>();

            var surveys = dbSet.Where(predicate);

            FillPopularBookVmList(popularBookVms, surveys);

            return popularBookVms.OrderBy(vm => vm.SoldBooksCount).ToList();
        }

        private static void FillPopularBookVmList<T>(IList<PopularBookVm> popularBookVms, IQueryable<T> surveys) where T: IBookContained
        {
            foreach (var groupByBook in surveys.GroupBy(survey => survey.Book))
            {
                popularBookVms.Add(new PopularBookVm
                {
                    Book = new BookVm(groupByBook.Key),
                    SoldBooksCount = groupByBook.Count()
                });
            }
        }
    }
}