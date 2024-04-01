using DataModel;
using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalApp.services
{
    public class DropDown
    {
        public static List<MasterModal> RoleDropdown()
        {

            List<MasterModal> RoleList = new List<MasterModal>();
            using (var context = new DataContextContainer())
            {

                var list = context.CodeController.Where(data => data.CodeName == "Occupation").ToList();
                foreach (var rlist in list)
                {
                    RoleList.Add(new MasterModal()
                    {
                        ID = Convert.ToInt32(rlist.CodeID),
                        Name = rlist.name

                    });
                }

            }
            return RoleList;
        }


        public static List<MasterModal> CountryDropdown()
        {

            List<MasterModal> CountryList = new List<MasterModal>();
            using (var context = new DataContextContainer())
            {

                var list = context.CodeController.Where(data => data.CodeName == "Country").ToList();
                foreach (var rlist in list)
                {
                    CountryList.Add(new MasterModal()
                    {
                        ID = Convert.ToInt32(rlist.CodeID),
                        Name = rlist.name

                    });
                }

            }
            return CountryList;
        }
        public static List<MasterModal> genderDropdown()
        {
            List<MasterModal> genderList = new List<MasterModal>();
            using (var context = new DataContextContainer())
            {
                var list = context.CodeController.Where(data => data.CodeName == "gender").ToList();
                foreach (var gender in list)
                {
                    genderList.Add(new MasterModal()
                    {
                        ID = Convert.ToInt32(gender.CodeID),
                        Name = gender.name

                    });
                }
            }
            return genderList;
        }

        public static List<MasterModal> BloodGroupDropdown()
        {
            List<MasterModal> BloodGroupList = new List<MasterModal>();
            using (var context = new DataContextContainer())
            {
                var list = context.CodeController.Where(data => data.CodeName == "BloodGroup").ToList();
                foreach (var blood in list)
                {
                    BloodGroupList.Add(new MasterModal()
                    {
                        ID = Convert.ToInt32(blood.CodeID),
                        Name = blood.name,

                    });
                }
            }

            return BloodGroupList;
        }
        public static List<MasterModal> QualificationDropdown()
        {
            List<MasterModal> QualList = new List<MasterModal>();
            using (var context = new DataContextContainer())
            {
                var list = context.CodeController.Where(data => data.CodeName == "Qualification").ToList();
                foreach (var qlist in list)
                {
                    QualList.Add(new MasterModal()
                    {
                        ID = Convert.ToInt32(qlist.CodeID),
                        Name = qlist.name

                    });
                }
            }
            return QualList;
        }
        public static List<MasterModal> DoctorCategoryDropDown()
        {
            List<MasterModal> DocCat = new List<MasterModal>();
            using (var context = new DataContextContainer())
            {
                var list = context.CodeController.Where(data => data.CodeName == "DoctorCategory").ToList();
                foreach (var qlist in list)
                {
                    DocCat.Add(new MasterModal()
                    {
                        ID = Convert.ToInt32(qlist.CodeID),
                        Name = qlist.name

                    });
                }
            }
            return DocCat;
        }
        public static List<MasterModal> DoctorByCategory(string name)
        {
            List<MasterModal> DocCat = new List<MasterModal>();
            using (var context = new DataContextContainer())
            {
                var list = context.EmployeeDetails.Where(data => data.Role== "Doctor" && data.Specialization==name).ToList();
                foreach (var qlist in list)
                {
                    DocCat.Add(new MasterModal()
                    {
                        ID = Convert.ToInt32(qlist.EmpID),
                        Name = qlist.Name

                    });
                }
            }
            return DocCat;
        }
        public bool createdropdown(DropDownCreation create)
        {
            bool status = false;
            using (var context = new DataContextContainer())
            {
                CodeController newdrop = new CodeController();
                newdrop.CodeName = create.CodeName;
                newdrop.name = create.Name;
                newdrop.CodeID = context.CodeController.Where(data => data.CodeName == create.CodeName).Count() + 1;
                context.CodeController.Add(newdrop);
                context.SaveChanges();
                status = true;
            }
            return status;
        }

        public static PagedData<DropDownCreation> DisplayDropDownMaster(int page, int pageSize, string CodeName = "")
        {
            var Dropdowndata = new PagedData<DropDownCreation>();
            List<DropDownCreation> dropdata = new List<DropDownCreation>();
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

                var query = context.CodeController.Where(C => C.CodeName.Contains(CodeName)).OrderBy(Data => Data.CodeName).Skip(skiprecord).Take(pageSize).ToList();
                if (query != null)
                {
                    foreach (var list in query)
                    {
                        dropdata.Add(new DropDownCreation()
                        {
                            CodeName = list.CodeName,
                            Name = list.name,
                            Description = list.Description,
                            codeId = Convert.ToInt32(list.CodeID),
                            ID = list.ID


                        });
                    }
                }
                var totalcount = 0;
                totalcount = context.CodeController.Where(C => C.CodeName.Contains(CodeName)).Count();
                Dropdowndata.TotalPages = Convert.ToInt32(Math.Ceiling((double)totalcount / pageSize));
                Dropdowndata.data = dropdata;
                Dropdowndata.pageSize = pageSize;
                Dropdowndata.visiblePages = 10;
                Dropdowndata.CurrentPage = page;

            }
            return Dropdowndata;
        }

        public static List<MasterModal> MonthDropdown()
        {
            List<MasterModal> Monthdata = new List<MasterModal>();
            using (var context = new DataContextContainer())
            {
                var query = context.CodeController.Where(data => data.CodeName == "Month").ToList();
                foreach (var list in query)
                {
                    Monthdata.Add(new MasterModal()
                    {
                        ID = Convert.ToInt32(list.CodeID),
                        Name = list.name

                    });
                }

            }
            return Monthdata;
        }
        public static List<MasterModal> SessionDropDown()
        {
            List<MasterModal> sessiondta = new List<MasterModal>();
            using (var context = new DataContextContainer())
            {
                var query = context.CodeController.Where(data => data.CodeName == "Session").ToList();
                foreach (var list in query)
                {
                    sessiondta.Add(new MasterModal()
                    {
                        ID = Convert.ToInt32(list.CodeID),
                        Name = list.name
                    });
                }
            }
            return sessiondta;
        }
        public static List<MasterModal> GetCodeNameList()
        {
            List<MasterModal> DocCat = new List<MasterModal>();
            using (var context = new DataContextContainer())
            {
                var list = context.CodeController.Select(data => data.CodeName).Distinct().ToList();
                foreach (var qlist in list)
                {
                    int indx = 1;
                    DocCat.Add(new MasterModal()
                    {
                        ID = indx,
                        Name = qlist

                    });
                    indx++;
                }
            }
            return DocCat;
        }
        public static List<MasterModal> GetSlotTimesForDoctorAndDate(int doctorid, string date)
        {
            List<MasterModal> timelist = new List<MasterModal>();
            using (var context = new DataContextContainer())
            {
               var query = from a in context.DoctorAvailabilityMaster
                                      join b in context.Doctorscheduledetails on a.AvailID equals b.AvailID
                                      join c in context.slotdetails on b.scheduleID equals c.scheduleID
                                      where a.DoctorID == doctorid && a.Date == date && c.status == "notbooked"
                           select new
                                      {
                                          c.slotID,
                                          c.slotTime
                                      };

                
                var results = query.ToList();
                foreach (var qlist in results)
                {
                    timelist.Add(new MasterModal()
                    {
                        ID = Convert.ToInt32(qlist.slotID),
                        Name = qlist.slotTime

                    });
                }
            }
            return timelist;
        }
        public static List<MasterModal> GetSlotDateForDoctor(int doctorid)
        {
            List<MasterModal> timelist = new List<MasterModal>();
            using (var context = new DataContextContainer())
            {
                var list = context.Availabilityslots.Where(data => data.doctorID == doctorid).Distinct();
                foreach (var qlist in list)
                {
                    timelist.Add(new MasterModal()
                    {
                        ID = Convert.ToInt32(qlist.slotID),
                        Name = qlist.Date

                    });
                }
            }
            return timelist;
        }

    }
}