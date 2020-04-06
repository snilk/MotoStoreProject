
using System;
using MotoStore.Domain.EF;

namespace MotoStore.Domain.ViewModels
{
    public class OrderAccountInfoVm
    {
        public OrderAccountInfoVm()
        {
            
        }
        public OrderAccountInfoVm(Order order)
        {
            Status = order.Status;
            HomeAddress = order.Address;
            OrderDate = order.OrderDate;
        }
        public bool Status { get; set; }
        public string HomeAddress { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
