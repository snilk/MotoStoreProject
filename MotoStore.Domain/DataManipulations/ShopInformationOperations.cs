using System.Collections.Generic;
using System.Linq;
using MotoStore.Domain.EF;
using MotoStore.Domain.ViewModels;

namespace MotoStore.Domain.DataManipulations
{
    public static class ShopInformationOperations
    {
        public static List<ShopInformationVm> GetShopInformation()
        {
            using (var context = new MotoStoreContext())
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