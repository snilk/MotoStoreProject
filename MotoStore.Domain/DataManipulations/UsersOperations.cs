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
            string keystart = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NSIsIm5h";
            string keylast = "bWUiOiJKb2huIEdvbGQiLCJhZG1pbiI6dHJ1ZX0.LIHjWCBORSWMEibq - tnT8ue_deUqZx1K0XxCOXZRrBI";
            return keystart + user.Id + keylast;
        }
        public static bool checkUserByToken(string token)
        {
            string keystart = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NSIsIm5h";
            string keylast = "bWUiOiJKb2huIEdvbGQiLCJhZG1pbiI6dHJ1ZX0.LIHjWCBORSWMEibq - tnT8ue_deUqZx1K0XxCOXZRrBI";
            int tokenId = Convert.ToInt32( token.Replace(keystart, "").Replace(keylast, ""));
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            return ((from u in context.Users where u.Id==tokenId select u).FirstOrDefault())!=null;
        }
    }
}
