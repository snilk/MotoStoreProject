using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStore.Domain.InterData
{
    public class OrderInfo
    {
        public string token { get; set; }
        public int idMoto { get; set; }
        public int idShop { get; set; }
        public string adress { get; set; }
    }
}
