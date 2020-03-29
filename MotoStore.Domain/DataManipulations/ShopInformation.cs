using System;
using System.Linq;
using MotoStore.Domain.EF;

namespace MotoStore.Domain
{
    public static class ShopInformation
    {
        public static Array getShopInformation()
        {
            var context = new MotoStoreContext();
            var shopInfo = (from s in context.ShopInformations
                select new
                {
                    s.Id, Address = s.Address, Phone1 = s.Phone1, Phone2 = s.Phone2
                }).ToArray();
            return shopInfo;
        }
    }
}