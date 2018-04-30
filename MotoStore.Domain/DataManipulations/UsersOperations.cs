using MotoStore.Domain.DataManipulations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStore.Domain
{
    public static class UsersOperations
    {

        public static bool? addNewUser(User newUser)
        {
            if (newUser == null) return null;
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            var isExist = (from u in context.Users where u.username == newUser.username select u.username).FirstOrDefault();
            if (isExist != null)
                return false;
            context.Users.Add(newUser);
            context.SaveChanges();
            return true;
        }
        public static UserTokenRole getUserToken(UserForAuthorization user)
        {
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            User existUser = (from u in context.Users where u.username == user.username select u).FirstOrDefault();
            if (existUser == null) return new UserTokenRole { correctUsername = false, correctPassword = null, token = null, role=null };
            if (existUser.password == user.password) return new UserTokenRole { role=existUser.role, token = getTokenById(existUser), correctPassword = true, correctUsername = true };
            else return new UserTokenRole { correctPassword = false, correctUsername = true, token = null,role=null };
            
        }
        public static string getTokenById(User user)
        {
            string keystart = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9eyJzdWIiOiIxMjM0NSIsIm5h";
            string keylast = "bWUiOiJKb2huIEdvbGQiLCJhZG1pbiI6dHJ1ZX0LIHjWCBORSWMEibq - tnT8ue_deUqZx1K0XxCOXZRrBI";
            return keystart + user.Id + keylast;
        }
        public static bool checkUserByToken(string token)
        {
            string keystart = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9eyJzdWIiOiIxMjM0NSIsIm5h";
            string keylast = "bWUiOiJKb2huIEdvbGQiLCJhZG1pbiI6dHJ1ZX0LIHjWCBORSWMEibq - tnT8ue_deUqZx1K0XxCOXZRrBI";
            int? tokenId = null;
            try
            {
                tokenId = Convert.ToInt32(token.Replace(keystart, "").Replace(keylast, ""));
            }
            catch (Exception)
            {
                tokenId = null;
            }
            if (tokenId == null) return false;
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            return ((from u in context.Users where u.Id==tokenId select u).FirstOrDefault())!=null;
        }
        public static int? getUserIdByToken(string token)
        {
            
            string keystart = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9eyJzdWIiOiIxMjM0NSIsIm5h";
            string keylast = "bWUiOiJKb2huIEdvbGQiLCJhZG1pbiI6dHJ1ZX0LIHjWCBORSWMEibq - tnT8ue_deUqZx1K0XxCOXZRrBI";
            int? tokenId = null;
            try
            {
                tokenId = Convert.ToInt32(token.Replace(keystart, "").Replace(keylast, ""));
            }
            catch (Exception)
            {
                tokenId = null;
            }
            if (tokenId == null) return null;
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            if ((from u in context.Users where u.Id == tokenId select u).FirstOrDefault() != null) return tokenId;
            return null;

        }
        public static Array getAccountInformation(string token)
        {
            int? id = getUserIdByToken(token);
            if (id == null) return null;
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            var orderInfo = from o in context.Orders
                            join m in context.Motorcycles on o.id_moto equals m.Id
                            join s in context.Shop_information on o.id_shop equals s.Id
                            join u in context.Users on o.id_user equals u.Id
                            where u.Id == (int)id
                            select new
                            {
                                motoId=m.Id,m.make,m.type,m.year_of_issue,m.price,m.main_photo,
                                shopAdress=s.adress,s.phone_1,s.phone_2,
                                orderId=o.Id,homeAdress=o.adress,o.status,o.date_compose
                            };
            var userAndOrdersInfo = (from u in context.Users
                           where u.Id == (int)id
                           select new
                           {
                               u.name,
                               u.surname,
                               u.phone,
                               u.email,
                               u.date_registration,
                               orders = orderInfo
                           }).ToArray();
            return userAndOrdersInfo;
        }
    }
}
