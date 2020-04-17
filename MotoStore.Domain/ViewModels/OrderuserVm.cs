using System;
using BookStore.Domain.EF;

namespace BookStore.Domain.ViewModels
{
    public class OrderUserVm
    {
        public OrderUserVm()
        {
            
        }

        public OrderUserVm(User user)
        {
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
            Phone = user.Phone;
            RegistrationDate = user.RegistrationDate;
        }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
