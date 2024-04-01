using DataModel;
using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace HospitalApp.services
{
    public class EmployeeServices
    {
        public bool CreateEmployee(EmployeeModal data)
        {
            bool status = false;
            using (var context = new DataContextContainer())
            {
                EmployeeDetails cr = new EmployeeDetails();
                cr.EmpCode = data.intEmpId.ToString();
                cr.Name = data.strEmpName;
                cr.Gender = data.byGender;
                cr.Address = data.strAddress;
                cr.Age = data.intAge;
                cr.Nationality = data.intNationality.ToString();
                cr.Password = data.strPassword;
                cr.UserName = data.strUserName;
                cr.Created_at = DateTime.Now;
                cr.Status = "active";
                cr.Role = data.Role;
                cr.PhoneNumber = data.strContactNo;
                cr.Qualification = data.strQualification;
                cr.Specialization = data.intSpecialization;
                cr.DOB = data.dtDob.ToString();
                cr.Doj = Convert.ToDateTime(data.dtDoj);
                cr.Email = data.strEmail;
                cr.BloodGroup = data.Bloodgrp;
                cr.MaritalStatus = data.byMaritalStatus;


                context.EmployeeDetails.Add(cr);
                context.SaveChanges();
                LoginCredentials Employee = new LoginCredentials()
                {
                    Username = data.strUserName,
                    Password = data.strPassword,
                    Role = data.Role,
                    EmployeeID = cr.EmpID
                };
                context.LoginCredentials.Add(Employee);
                context.SaveChanges();

                status = true;
            }
            return status;

        }
        public List<EmployeeModal> EmployeeList()
        {
            List<EmployeeModal> displaylist = new List<EmployeeModal>();
            using (var context = new DataContextContainer())
            {
                var query = context.EmployeeDetails.Where(data => data.Status == "active").ToList();

                foreach (var employee in query)
                {
                    displaylist.Add(new EmployeeModal()
                    {
                        Id = employee.EmpID,
                        Role = employee.Role,
                        strEmpName = employee.Name,
                        strQualification = employee.Qualification,
                        strContactNo = employee.PhoneNumber,
                        intEmpId = employee.EmpCode,

                    });
                }
            }
            return displaylist;
        }


        public static PagedData<EmployeeModal> EmpListPD(int page = 0)
        {
            PagedData<EmployeeModal> PDList = new PagedData<EmployeeModal>();
            List<EmployeeModal> EmpList = new List<EmployeeModal>();
            try
            {
                int Listsize = 3;
                int skipCount = 0;
                if (page == 0)
                {
                    skipCount = page;
                }
                else
                {
                    skipCount = page * Listsize;
                }
                using (var context = new DataContextContainer())
                {

                    var query = context.EmployeeDetails.
                          Where(data => data.Status == "active").ToList();
                    PDList.TotalPages = query.Count();

                    query = query.Skip(skipCount).Take(Listsize).ToList();

                    foreach (var employee in query)
                    {
                        EmpList.Add(new EmployeeModal
                        {
                            Id = employee.EmpID,
                            Role = employee.Role,
                            strEmpName = employee.Name,
                            strQualification = employee.Qualification,
                            strContactNo = employee.PhoneNumber,
                            intEmpId = employee.EmpCode,

                        });
                    }
                    PDList.TotalPages = query.Count();
                    PDList.data = EmpList;
                    PDList.visiblePages = Listsize;
                    PDList.pageSize = 3;

                }
                return PDList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeleteEmployee(int id)
        {
            bool status = false;
            using (var context = new DataContextContainer())
            {
                var employee = context.EmployeeDetails.FirstOrDefault(data => data.EmpID == id);
                if (employee != null)
                {
                    employee.Status = "inactive";
                    status = true;
                    context.SaveChanges();
                }

            }
            return status;
        }
        public static EmployeeModal UpdateEmployeeDetails(int id)
        {

            EmployeeModal values = new EmployeeModal();
            using (var context = new DataContextContainer())
            {
                var employee = context.EmployeeDetails.FirstOrDefault(data => data.EmpID == id);
                if (employee != null)
                {
                    values.Id = employee.EmpID;
                    values.intEmpId = employee.EmpCode;
                    values.Bloodgrp = employee.BloodGroup;
                    values.byGender = employee.Gender;
                    values.byMaritalStatus = employee.MaritalStatus;
                    values.intAge = Convert.ToInt32(employee.Age);
                    values.intNationality = employee.Nationality;
                    values.strAddress = employee.Address;
                    values.strContactNo = employee.PhoneNumber;
                    values.strEmail = employee.Email;
                    values.strUserName = employee.UserName;
                    values.strEmpName = employee.Name;
                    values.intSpecialization = employee.Specialization;
                    values.Role = employee.Role;
                    values.dtDob = Convert.ToDateTime(employee.DOB);
                    values.dtDoj = employee.Doj.ToString();
                    values.strQualification = employee.Qualification;
                }
            }
            return values;
        }

        public static PagedData<EmployeeModal> DisplayEmployeeListModel(int page, int pageSize, string Role = "")
        {

            PagedData<EmployeeModal> empdata = new PagedData<EmployeeModal>();
            List<EmployeeModal> EmployeeData = new List<EmployeeModal>();
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


                var query = context.EmployeeDetails.Where(m => m.Role.Contains(Role)).OrderBy(m => m.Name).Skip(skiprecord).Take(pageSize).ToList();
                if (query != null)
                {
                    foreach (var list in query)
                    {
                        EmployeeData.Add(new EmployeeModal()
                        {
                            intEmpId = list.EmpCode,
                            strEmpName = list.Name,
                            strQualification = list.Qualification,
                            Role = list.Role,

                        });
                        var totalcount = 0;
                        totalcount = context.EmployeeDetails.Where(m => m.Role.Contains(Role)).Count();
                        empdata.pageSize = pageSize;
                        empdata.TotalPages = Convert.ToInt32(Math.Ceiling((double)totalcount / pageSize));
                        empdata.data = EmployeeData;

                        empdata.visiblePages = 3;
                        empdata.CurrentPage = page;
                    }
                }

            };
            return empdata;
        }
        public bool updateEmployee(EmployeeModal model)
        {
            bool status = false;

            using (var context = new DataContextContainer())
            {
                var query = context.EmployeeDetails.FirstOrDefault(data => data.EmpID == model.Id);
                EmployeeDetails cr = new EmployeeDetails();

                query.EmpCode = model.intEmpId;
                query.Name = model.strEmpName;
                query.Gender = model.byGender;
                query.Address = model.strAddress;
                query.Age = model.intAge;
                query.Nationality = model.intNationality.ToString();
                query.Password = model.strPassword;
                query.UserName = model.strUserName;
                query.Created_at = DateTime.Now;
                query.Status = "active";
                query.Role = model.Role;
                query.PhoneNumber = model.strContactNo;
                query.Qualification = model.strQualification;
                query.Specialization = model.intSpecialization;
                query.DOB = model.dtDob.ToString();
                query.Doj = Convert.ToDateTime(model.dtDoj);
                query.Email = model.strEmail;
                query.BloodGroup = model.Bloodgrp;
                query.MaritalStatus = model.byMaritalStatus;



                context.SaveChanges();
                status = true;
            }
            return status;
        }

        public List<EmployeeModal> getRole()
        {
            List<EmployeeModal> RoleData = new List<EmployeeModal>();
            using (var context = new DataContextContainer())
            {
                var query = context.EmployeeDetails.ToList();
                foreach (var list in query)
                {
                    RoleData.Add(new EmployeeModal()
                    {
                        Role = list.Role
                    });
                }
            }
            return RoleData;
        }
        public static bool DoctorScheduleAvailability1(DoctorAvailability su, int id, int SlotTime)
        {
            bool status = false;
            using (var context = new DataContextContainer())
            {
                DoctorAvailabilityMaster cr = new DoctorAvailabilityMaster();
                cr.DoctorID = id;
                cr.status = "Active";
                cr.Date = su.Shift[0].Date;
                context.DoctorAvailabilityMaster.Add(cr);
                context.SaveChanges();


                foreach (var data in su.Shift)
                {

                    Doctorscheduledetails create = new Doctorscheduledetails();
                    create.AvailID = cr.AvailID;

                    create.session = data.Session;
                    create.Duration = data.Time;
                    context.Doctorscheduledetails.Add(create);
                    context.SaveChanges();


                    var MorningTime = new TimeSpan(9, 0, 0);
                    var AfterNoonTime = new TimeSpan(2, 0, 0);
                    var EveningTime = new TimeSpan(6, 0, 0);
                    for (var i = 0; i < ((data.Time * 60) / SlotTime); i++)
                    {
                        slotdetails creation = new slotdetails();
                        creation.AvailID = cr.AvailID;
                        creation.scheduleID = create.scheduleID;
                        creation.canceldescription = null;
                        creation.description = null;
                        creation.PatientID = null;
                        creation.status = "notbooked";

                        if (data.Session == "Morning")
                        {
                            var slotStartTime = MorningTime.Add(TimeSpan.FromMinutes(i * SlotTime));
                            creation.slotTime = Convert.ToString(slotStartTime) + "AM";
                        }
                        else if (data.Session == "AfterNoon")
                        {
                            var slotStartTime = AfterNoonTime.Add(TimeSpan.FromMinutes(i * SlotTime));
                            creation.slotTime = Convert.ToString(slotStartTime) + "PM";
                        }
                        else if (data.Session == "Evening")
                        {
                            var slotStartTime = EveningTime.Add(TimeSpan.FromMinutes(i * SlotTime));
                            creation.slotTime = Convert.ToString(slotStartTime) + "PM";

                        }
                        context.slotdetails.Add(creation);
                        context.SaveChanges();
                    }
                }
            }
            return status;
        }
        public static bool DoctorScheduleAvailability(DoctorAvailability su, int id, int SlotTime)
        {
            bool status = false;

            using (var context = new DataContextContainer())
            {

                foreach (var data in su.Shift)
                {
                    var count = context.DoctorSchedule.Where(s => s.Date == data.Date && s.doctorID
                    == id).Count();
                    if (count < 3)
                    {
                        DoctorSchedule cr = new DoctorSchedule();

                        cr.doctorID = id;
                        cr.Duration = data.Time;
                        cr.Date = data.Date;
                        cr.Session = data.Session;
                        context.DoctorSchedule.Add(cr);
                        context.SaveChanges();
                        status = true;
                        var MorningTime = new TimeSpan(9, 0, 0);
                        var AfterNoonTime = new TimeSpan(2, 0, 0);
                        var EveningTime = new TimeSpan(6, 0, 0);

                        for (var i = 0; i < ((data.Time * 60) / SlotTime); i++)
                        {
                            Availabilityslots create = new Availabilityslots();
                            create.Date = data.Date;
                            create.doctorID = id;
                            create.patientID = null;
                            create.session = data.Session;
                            if (data.Session == "Morning")
                            {
                                var slotStartTime = MorningTime.Add(TimeSpan.FromMinutes(i * SlotTime));
                                create.slotTime = Convert.ToString(slotStartTime) + "AM";
                            }
                            else if (data.Session == "AfterNoon")
                            {
                                var slotStartTime = AfterNoonTime.Add(TimeSpan.FromMinutes(i * SlotTime));
                                create.slotTime = Convert.ToString(slotStartTime) + "PM";
                            }
                            else if (data.Session == "Evening")
                            {
                                var slotStartTime = EveningTime.Add(TimeSpan.FromMinutes(i * SlotTime));
                                create.slotTime = Convert.ToString(slotStartTime) + "PM";

                            }
                            create.status = "Active";

                            context.Availabilityslots.Add(create);
                            context.SaveChanges();
                        }

                    }
                }
            }
            return status;
        }
        public static PagedData<Availablityslotbooking> displayslots(int page, int pageSize, int id, string date = "", string session = "")
        {
            PagedData<Availablityslotbooking> list = new PagedData<Availablityslotbooking>();

            List<Availablityslotbooking> schedulelist = new List<Availablityslotbooking>();

            using (var context = new DataContextContainer())
            {
                int skiprecord = (page - 1) * pageSize;
                var query = context.Availabilityslots.Where(data => data.doctorID == id && (data.status == "Active" || data.status == "Transferred" || data.status == "Approved") && data.Date.Contains(date) && data.session.Contains(session)).OrderBy(data => data.Date).Skip(skiprecord).Take(pageSize).ToList();


                foreach (var data in query)
                {
                    schedulelist.Add(new Availablityslotbooking()
                    {
                        doctorID = Convert.ToInt32(data.doctorID),
                        session = data.session,
                        Date = data.Date,
                        slotTime = data.slotTime,
                        patientID = Convert.ToInt32(data.patientID),
                        scheduleID = Convert.ToInt32(data.slotID),
                        status = data.status


                    });
                }
                var totalcount = 0;
                totalcount = context.Availabilityslots.Where(data => data.doctorID == id).Count();
                list.pageSize = pageSize;
                list.TotalPages = Convert.ToInt32(Math.Ceiling((double)totalcount / pageSize));
                list.data = schedulelist;

                list.visiblePages = 10;
                list.CurrentPage = page;
            }
            return list;
        }
        public static byte deleteschedule(int id)
        {
            byte status = 0;
            using (var context = new DataContextContainer())
            {
                var query = context.Availabilityslots.Where(data => data.slotID == id).FirstOrDefault();

                query.status = "inactive";
                context.SaveChanges();
                status = 1;
            }
            return status;
        }
        public static byte Leaveschedule(int id)
        {
            byte status = 0;
            using (var context = new DataContextContainer())
            {
                var query = context.Availabilityslots.Where(data => data.slotID == id).FirstOrDefault();

                query.status = "Transferred";
                context.SaveChanges();
                status = 1;
            }
            return status;
        }

        public static bool bookingAppointment(AppointmentModel cr,int patid)
        {
            bool status = false;
            using (var context = new DataContextContainer())
            {
                var query = context.slotdetails.Where(data => data.slotID == cr.Time).FirstOrDefault();
                
                query.status = "Booked";
                query.description = cr.Description;
                query.PatientID = patid;
                context.SaveChanges();
                AppointmentBooking appointmentBooking = new AppointmentBooking();
                appointmentBooking.Date = cr.date;
                appointmentBooking.DoctorID = cr.Doctor;
                appointmentBooking.Description=cr.Description;
                appointmentBooking.patientID = patid;
                appointmentBooking.Time = cr.Time;
                appointmentBooking.status = "Booked";
              
                context.AppointmentBooking.Add(appointmentBooking);
                context.SaveChanges();

               
                
                
                status = true;

            }
            return status;
        }
        public static bool cancelAppointmentForPatient(int slotid)
        {
            bool status = false;
            using (var context = new DataContextContainer())
            {
                var slotdetails= context.slotdetails.Where(data=>data.slotID == slotid).FirstOrDefault();
                slotdetails.status= "Cancel";
                slotdetails.description = "";
                slotdetails.PatientID = 0;
                context.SaveChanges();
                var appointmentbooking =context.AppointmentBooking.Where(data=>data.Time==slotid).FirstOrDefault();
                appointmentbooking.status= "cancel";
                context.SaveChanges();

                status = true;
            }
            return status;

        }
        public static PagedData<AppointmentModel> getAppointmentListForPatient(int patid)
        {
            PagedData<AppointmentModel> details= new PagedData<AppointmentModel>();
            List<AppointmentModel> list = new List<AppointmentModel>();

            using(var context= new DataContextContainer())
            {
                var query = context.AppointmentBooking.Where(data => data.patientID == patid && data.status=="Booked").ToList();
                
                if(query != null)
                {
                    foreach(var item in query)
                    {
                        list.Add(new AppointmentModel()
                        {
                            Description = item.Description,
                            date = item.Date,
                            Time = Convert.ToInt32(item.Time)
                        });
                    }
                    details.data= list;
                }
            }

            return details;
        } 
        public static List<DoctorAvailability> AvailabilityList()
        {
            List<DoctorAvailability> list = new List<DoctorAvailability>();
            using (var context = new DataContextContainer())
            {
                var query = from one in context.DoctorAvailabilityMaster
                            join two in context.Doctorscheduledetails on one.AvailID equals two.AvailID
                            select new
                            {
                                Date = one.Date,
                                Session = two.session,
                                Duration = two.Duration
                            };
                var qrylist = query.ToList();
                if (qrylist.Count > 0)
                {
                    for (int i = 0; i < qrylist.Count; i++)
                    {
                        list.Add(new DoctorAvailability()
                        {
                            Date = qrylist[i].Date,
                            Session = qrylist[i].Session,
                            Time = Convert.ToInt32(qrylist[i].Duration)
                        });

                    }


                }

                
            }
            return list;
        }

        public static PagedData<Availablityslotbooking> getDoctorAvailability(int docid)
        {
            PagedData<Availablityslotbooking> list = new PagedData<Availablityslotbooking>();

            List<Availablityslotbooking> schedulelist = new List<Availablityslotbooking>();
            using(var context= new DataContextContainer())
            {
              
                var query = from avail in context.DoctorAvailabilityMaster
                            join schedule in context.Doctorscheduledetails
                            on avail.AvailID equals schedule.AvailID
                            join slot in context.slotdetails
                            on avail.AvailID equals slot.AvailID
                            where avail.DoctorID==docid &&
                             schedule.scheduleID==slot.scheduleID
                            select new
                            {
                                avail.AvailID,
                                avail.DoctorID,
                                avail.status,
                                avail.Date,
                                schedule.session,
                                slot.slotTime
                            };

                var result = query.ToList();

                foreach (var data in result) {
                    schedulelist.Add(new Availablityslotbooking()
                    {
                        doctorID = Convert.ToInt32(data.DoctorID),
                        AvailabilityID = data.AvailID,
                        Date = data.Date,
                        status = data.status,
                        session=data.session,
                        slotTime=data.slotTime
                    });

                }

            }
            list.data = schedulelist;
            
            return list;
        }
        public static PagedData<Availablityslotbooking> getSession(int docid,string session)
        {
            PagedData<Availablityslotbooking> list = new PagedData<Availablityslotbooking>();

            List<Availablityslotbooking> schedulelist = new List<Availablityslotbooking>();
            using (var context = new DataContextContainer())
            {

                var query = from availability in context.DoctorAvailabilityMaster
                            join schedule in context.Doctorscheduledetails on availability.AvailID equals schedule.AvailID
                            join slot in context.slotdetails on availability.AvailID equals slot.AvailID
                            where availability.DoctorID == docid
                                  && schedule.session == session
                                  && schedule.scheduleID == slot.scheduleID
                            select new
                            {
                                availability.Date,
                                schedule.session,
                                slot.slotTime
                            };

                var result = query.ToList();

                foreach (var data in result)
                {
                    schedulelist.Add(new Availablityslotbooking()
                    {
                        Date=data.Date,
                        session = data.session,
                        slotTime = data.slotTime
                    });

                }

            }
            list.data = schedulelist;

            return list;
        }
    }
}
