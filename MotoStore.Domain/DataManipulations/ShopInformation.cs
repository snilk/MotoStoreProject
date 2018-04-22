using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStore.Domain
{
    public static class ShopInformation
    {
        public static Array getShopInformation()
        {
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            var shopInfo = (from s in context.Shop_information
                           select new
                           {
                               s.Id,s.adress,s.phone_1,s.phone_2
                           }).ToArray();
            return shopInfo;
                        
        }
    }
}
