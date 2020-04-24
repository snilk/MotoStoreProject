using System.Collections.Generic;
using System.Linq;
using BookStore.Domain.EF;
using BookStore.Domain.ViewModels;

namespace BookStore.Domain.DataManipulations
{
    public static class ShopInformationOperations
    {
        public static List<ShopInformationVm> GetShopInformation()
        {
            using (var context = new BookStoreContext())
            {
                return context.ShopInformations.ToList()
                    .Select(shop => new ShopInformationVm
                    {
                        Id = shop.Id,
                        Phone2 = shop.Phone2,
                        Phone1 = shop.Phone1,
                        Address = shop.Address
                    }).ToList();
            }
        }
    }
}