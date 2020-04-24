using System.Collections.Generic;
using System.Linq;
using BookStore.Domain.EF;
using BookStore.Domain.ViewModels;

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
                    return new SuccessVm(false);
                }

                changedOrder.Status = !changedOrder.Status;
                var book = changedOrder.Book;

                var offSet = changedOrder.Status ? -1 : 1;
                book.ModelsCount += offSet;

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
    }
}