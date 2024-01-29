using System;
using TP_Model;
using TP_DAL;

namespace TP_BLL
{
    public static class UserManager
    {
        public static bool VerifyLoginPassword(string login, string password)
        {
            return UserServices.VerifyLoginPassword(login, password);  
        }

        public static bool RegisterNewUser(User user)
        {
            if (!UserServices.IsNameTaken(user))
            {
                return UserServices.RegisterUser(user);
            }
            else return false;
        }

        public static Guid GetId(string username)
        {
            return UserServices.GetId(username);
        }

    }
}
