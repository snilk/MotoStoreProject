using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStore.Domain.ViewModels
{
    public class OrderInfoAdminVm
    {
        public int orderId { get; set; }
        public int userId { get; set; }
        public int motoId { get; set; }
        public string homeAdress { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string shopAdress { get; set; }
        public string Phone1 { get; set; }
    }
}
