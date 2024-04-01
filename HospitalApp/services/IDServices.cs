using DataModel;
using HospitalApp.Models;
using System;
using System.Linq;

namespace HospitalApp.services
{
    public class IDServices
    {
        public static int GetId(LoginModal su)
        {
            var id = 0;
            try
            {

                using (var context = new DataContextContainer())
                {
                    var query = context.PatientDetails.FirstOrDefault(data => data.UserName == su.UserName && data.Password == su.PassWord);
                    if (query != null)
                    {
                        id = query.PatID;
                        return id;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



            return id;

        }
        public static int GetEmpId(LoginModal su)
        {
            var id = 0;
            try
            {

                using (var context = new DataContextContainer())
                {
                    var query = context.EmployeeDetails.FirstOrDefault(data => data.UserName == su.UserName && data.Password == su.PassWord);
                    if (query != null)
                    {
                        id = query.EmpID;
                        return id;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



            return id;

        }

        public static int GetEmployeeId(string name)
        {
            var id = 0;
            try
            {

                using (var context = new DataContextContainer())
                {
                    var query = context.EmployeeDetails.FirstOrDefault(data => data.UserName == name);
                    if (query != null)
                    {
                        id = query.EmpID;
                        return id;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



            return id;

        }

        public static int GetPatientId(string name)
        {
            var id = 0;
            try
            {

                using (var context = new DataContextContainer())
                {
                    var query = context.PatientDetails.FirstOrDefault(data => data.UserName == name);
                    if (query != null)
                    {
                        id = query.PatID;
                        return id;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



            return id;

        }


    }
}