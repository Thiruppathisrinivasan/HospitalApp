using DataModel;
using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace HospitalApp.services
{
    public class Signupservices
    {

        public bool getDetails(Signup Details)
        {
            bool status = false;
            using (var context = new DataContextContainer())
            {
                PatientDetails cr = new PatientDetails();

                cr.Name = Details.strName;
                cr.Age = Details.intAge;
                cr.BloodGroup = Details.intBloodGroup;
                cr.DOB = Details.dtDob.ToString();
                cr.MaritalStatus = Details.bytMaritalStatus;
                cr.Nationality = Details.intNationalty;
                cr.PhoneNumber = Details.strContact;
                cr.Password = Details.strPassword;
                cr.Status = "active";
                cr.Created_at = DateTime.Now;
                cr.Email = Details.strEmail;
                cr.Gender = Details.bytGender;
                cr.UserName = Details.strUserName;
                cr.Address = Details.strAddress;
                cr.Role = "Patient";
                context.PatientDetails.Add(cr);

                LoginCredentials logindata = new LoginCredentials()
                {
                    Username = Details.strUserName,
                    Password = Details.strPassword,
                    Role = "Patient",
                    PatientID = cr.PatID



                };

                context.LoginCredentials.Add(logindata);
                context.SaveChanges();
                status = true;
            }
            return status;
        }

        public List<Signup> Patientdashboard()
        {

            List<Signup> displaylist = new List<Signup>();
            using (var context = new DataContextContainer())
            {
                var Patients = context.PatientDetails.Where(data => data.Status == "active").ToList();
                foreach (var patlist in Patients)
                {
                    displaylist.Add(new Signup()
                    {
                        strName = patlist.Name,
                        strContact = patlist.PhoneNumber,
                        bytGender = patlist.Gender,
                        intBloodGroup = patlist.BloodGroup,
                        patID = patlist.PatID

                    });
                }
            }

            return displaylist;
        }

        public static Signup updatePatientDetails(int id)
        {
            Signup values = new Signup();
            using (var context = new DataContextContainer())
            {
                var patient = context.PatientDetails.FirstOrDefault(data => data.PatID == id);
                if (patient != null)
                {
                    values.patID = patient.PatID;
                    values.bytGender = patient.Gender;
                    values.bytMaritalStatus = patient.MaritalStatus;
                    values.dtDob = Convert.ToDateTime(patient.DOB);
                    values.intAge = Convert.ToInt32(patient.Age);
                    values.intBloodGroup = patient.BloodGroup;
                    values.intNationalty = patient.Nationality;
                    values.strAddress = patient.Address;
                    values.strContact = patient.PhoneNumber;
                    values.strEmail = patient.Email;
                    values.strName = patient.Name;
                    values.strUserName = patient.UserName;
                    values.Role = patient.Role;

                }
            }

            return values;
        }

        public bool UpdatePatientDB(Signup model)
        {
            bool status = false;
            using (var context = new DataContextContainer())
            {
                var patient = context.PatientDetails.FirstOrDefault(data => data.PatID == model.patID);

                if (patient != null)
                {
                    patient.MaritalStatus = model.bytMaritalStatus;
                    patient.Gender = model.bytGender;
                    patient.Address = model.strAddress;
                    patient.Age = model.intAge;
                    patient.BloodGroup = model.intBloodGroup;
                    patient.DOB = Convert.ToString(model.dtDob);
                    patient.Email = model.strEmail;
                    patient.Name = model.strName;
                    patient.Nationality = model.intNationalty;
                    patient.PhoneNumber = model.strContact;
                    context.SaveChanges();
                    status = true;
                }

            }
            return status;
        }

        public bool DeletePatient(int id)
        {
            bool status = false;
            using (var context = new DataContextContainer())
            {
                var Patient = context.PatientDetails.FirstOrDefault(data => data.PatID == id);

                Patient.Status = "inactive";
                context.SaveChanges();
                status = true;
            }
            return status;

        }

        public static PagedData<Signup> DisplayPatientList(int page, int pageSize, string BloodGroup)
        {
            PagedData<Signup> patientData = new PagedData<Signup>();
            List<Signup> patientList = new List<Signup>();
            int skiprecord = 0;
            if (page == 1)
            {
                skiprecord = 0;

            }
            else
            {
                skiprecord = (page - 1) * pageSize;
            }
            using (var context = new DataContextContainer())
            {
                var query = context.PatientDetails.Where(data => data.Name.Contains(BloodGroup)).OrderBy(data => data.Name).Skip(skiprecord).Take(pageSize).ToList();
                foreach (var list in query)
                {
                    patientList.Add(new Signup()
                    {
                        strName = list.Name,
                        bytGender = list.Gender,
                        intBloodGroup = list.BloodGroup,
                        strContact = list.PhoneNumber
                    });

                }
                var totalcount = 0;
                totalcount = context.PatientDetails.Where(data => data.Name.Contains(BloodGroup)).Count();
                patientData.data = patientList;
                patientData.pageSize = pageSize;
                patientData.TotalPages = Convert.ToInt32(Math.Ceiling((double)totalcount / pageSize));
                patientData.visiblePages = 10;
                patientData.CurrentPage = page;
            }
            return patientData;
        }

    }
}