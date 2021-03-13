using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.ViewModels
{
    public class PopularBookVm
    {
        public BookVm Book { get; set; }

        public int SoldBooksCount { get; set; }
    }
}
