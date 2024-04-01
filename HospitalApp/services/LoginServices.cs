using DataModel;
using HospitalApp.Models;
using System;
using System.Linq;

namespace HospitalApp.services
{
    public class LoginServices
    {
        public static bool Loginfun(LoginModal logindata)
        {
            bool status = false;

            using (var context = new DataContextContainer())
            {
                var query = context.LoginCredentials.FirstOrDefault(data => data.Username == logindata.UserName && data.Password == logindata.PassWord);
                if (query != null)
                {
                    status = true;

                }
            }
            return status;
        }

        public static string RoleLogin(LoginModal logindata)
        {
            var Role = string.Empty;
            using (var context = new DataContextContainer())
            {
                var query = context.LoginCredentials.FirstOrDefault(data => data.Username == logindata.UserName && data.Password == logindata.PassWord);
                if (query != null)
                {
                    Role = query.Role;
                }
            }
            return Role;
        }
    }
}
