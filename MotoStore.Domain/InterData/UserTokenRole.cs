using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStore.Domain.DataManipulations
{
  public  class UserTokenRole
    {
        public bool? role { get; set; }
        public string token { get; set; }
        public bool? correctUsername { get; set; }
        public bool? correctPassword { get; set; }
    }
}
