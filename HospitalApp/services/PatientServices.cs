using DataModel;
using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalApp.services
{
    public class PatientServices
    {
        public static List<Signup> Patientdata(int id)
        {

            List<Signup> patdata = new List<Signup>();
            using (var context = new DataContextContainer())
            {
                var Patients = context.PatientDetails.Where(data => data.PatID == id).FirstOrDefault();

                patdata.Add(new Signup()
                {
                    strName = Patients.Name,
                    strContact = Patients.PhoneNumber,

                });
            }
            return patdata;
        }


    }
}
