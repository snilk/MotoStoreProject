using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Static
{
    public static class PurchaseGoals
    {
        public static readonly IList<string> Goals = new List<string>()
        {
            "Саморазвите",
            "Для проекта"
        };
    }
}
