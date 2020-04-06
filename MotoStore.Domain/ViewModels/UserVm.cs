
using System;

namespace MotoStore.Domain.ViewModels
{
    public class UserVm
    {
        public UserVm()
        {
            
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool? IsAdmin { get; set; }
        public string Email { get; set; }
    }
}
