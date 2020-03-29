using System.Collections.Generic;
using System.Linq;
using MotoStore.Domain.EF;
using MotoStore.Domain.Static;
using MotoStore.Domain.ViewModels;

namespace MotoStore.Domain.DataManipulations
{
    public class AdminOperations
    {
        public static AdminInformationVm GetInfoFormAdmin()
        {
            var context = new MotoStoreContext();

            var orderList = OrderOperations.GetAllOrders();

            var shopInformations = context.ShopInformations.ToList().Select(info => new ShopInformationVm()
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
                shopInformations = shopInformations
            };
            return adminInformationVm;
        }

        public static SuccessVm ChangeOrderStatus(int id)
        {
            var context = new MotoStoreContext();
            var changedOrder = context.Orders.FirstOrDefault(o => o.Id == id);

            if (changedOrder == null)
            {
                return new SuccessVm(false);
            }
            //changedOrder.Status = changedOrder.Status ? false : true;
            if (changedOrder.Status)
            {
                changedOrder.Status = false;
                var degMoto =
                    context.Motorcycles.FirstOrDefault(m => m.Id == changedOrder.MotoId);
                degMoto.ModelsCount = degMoto.ModelsCount == 0 ? 0 : --degMoto.ModelsCount;
            }
            else
            {
                changedOrder.Status = true;
                context.Motorcycles.FirstOrDefault(m => m.Id == changedOrder.MotoId).ModelsCount += 1;
            }

            context.SaveChanges();
            return new SuccessVm(true);
        }

        public static SuccessVm RemoveMotoById(int id)
        {
            var context = new MotoStoreContext();
            var removedMoto = context.Motorcycles.FirstOrDefault(m => m.Id == id);
            if (removedMoto == null)
            {
                return new SuccessVm(false);
            }

            context.Motorcycles.Remove(removedMoto);
            context.SaveChanges();
            return new SuccessVm(true);
        }

        public static SuccessVm AddNewMoto(Motorcycle moto)
        {
            var context = new MotoStoreContext();
            moto.MainImage = MotoImagesConstants.PlaceHolderImage;
            var imageList = new List<MotoImage>
            {
                new MotoImage
                {
                    ImageUrl = MotoImagesConstants.PlaceHolderImage
                }
            };
            moto.MotoImages = imageList;
            context.Motorcycles.Add(moto);
            context.SaveChanges();
            return new SuccessVm(true);
        }

        public static SuccessVm AddShop(ShopInformationVm shopInformationVm)
        {
            var context = new MotoStoreContext();
            context.ShopInformations.Add(new EF.ShopInformation
            {
                Address = shopInformationVm.Address,
                Phone1 = shopInformationVm.Phone1,
                Phone2 = shopInformationVm.Phone2
            });
            context.SaveChanges();

            return new SuccessVm(true);
        }

        public static SuccessVm RemoveShopById(int id)
        {
            var context = new MotoStoreContext();
            var shopForRemove = context.ShopInformations.FirstOrDefault(m => m.Id == id);
            if (shopForRemove == null)
            {
                return new SuccessVm(false);
            }

            context.ShopInformations.Remove(shopForRemove);
            context.SaveChanges();
            return new SuccessVm(true);
        }
    }
}