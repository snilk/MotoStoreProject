﻿using BookStore.Domain.EF;

namespace BookStore.Domain.ViewModels
{
    public class ShopInformationVm
    {
        public ShopInformationVm()
        {
            
        }

        public ShopInformationVm(ShopInformation shopInformation)
        {
            Id = shopInformation.Id;
            Address = shopInformation.Address;
            Phone1 = shopInformation.Phone1;
            Phone2 = shopInformation.Phone2;
        }
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
    }

}