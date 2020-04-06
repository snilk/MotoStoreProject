using System.Linq;
using MotoStore.Domain.EF;
using MotoStore.Domain.ViewModels;

namespace MotoStore.Domain.DataManipulations
{
    public class AdminOperations
    {
        public static AdminInformationVm GetInfoFormAdmin()
        {
            var context = new MotoStoreContext();

            var orderList = OrderOperations.GetAllOrders();

            var shopInformationVms = context.ShopInformations.ToList().Select(info => new ShopInformationVm
            {
                Phone1 = info.Phone1,
                Address = info.Address,
                Phone2 = info.Phone2,
                Id = info.Id
            }).ToList();

            var motoList = context.Motorcycles.ToList().Select(moto=> new MotorcycleVm(moto)).ToList();
            var adminInformationVm = new AdminInformationVm
            {
                motos = motoList,
                orders = orderList,
                shopInformations = shopInformationVms
            };
            return adminInformationVm;
        }

        public static SuccessVm ChangeOrderStatus(int id)
        {
            using (var context = new MotoStoreContext())
            {
                var changedOrder = context.Orders.FirstOrDefault(o => o.Id == id);

                if (changedOrder == null)
                {
                    return new SuccessVm(false);
                }

                changedOrder.Status = !changedOrder.Status;
                var motorcycle = changedOrder.Motorcycle;

                var offSet = changedOrder.Status ? -1 : 1;
                motorcycle.ModelsCount += offSet;

                context.SaveChanges();
            }

            return new SuccessVm(true);
        }

        public static SuccessVm RemoveMotoById(int id)
        {
            using (var context = new MotoStoreContext())
            {
                var removedMoto = context.Motorcycles.FirstOrDefault(m => m.Id == id);

                if (removedMoto == null)
                {
                    return new SuccessVm(false);
                }

                context.MotoImages.RemoveRange(removedMoto.MotoImages);
                context.MotoImages.Remove(removedMoto.MainImage);
                context.Motorcycles.Remove(removedMoto);
                context.SaveChanges();
            }

            return new SuccessVm(true);
        }

        public static SuccessVm AddNewMoto(MotorcycleVm motorcycleVm, string imagesPath)
        {
            using (var context = new MotoStoreContext())
            {
                context.Motorcycles.Add(MotorcycleOperations.CreateNewMotorcycle(motorcycleVm, imagesPath));
                context.SaveChanges();
            }

            return new SuccessVm(true);
        }

        public static SuccessVm AddShop(ShopInformationVm shopInformationVm)
        {
            using (var context = new MotoStoreContext())
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
            using (var context = new MotoStoreContext())
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