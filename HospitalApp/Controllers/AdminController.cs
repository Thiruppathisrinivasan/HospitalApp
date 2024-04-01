using HospitalApp.Models;
using HospitalApp.services;
using System;
using System.Linq;
using System.Web.Mvc;


namespace HospitalApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult AdminDashboard(int? page)
        {


            EmployeeServices display = new EmployeeServices();
            DashboardModel mdl = new DashboardModel();

            //mdl.EmpList= display.EmployeeList();

            mdl.EmpList = EmployeeServices.EmpListPD();
            Signupservices patientdisplay = new Signupservices();

            mdl.PatList = patientdisplay.Patientdashboard();




            return View(mdl);
        }

        public ActionResult EmployeeCreation(int id = 0)
        {

            EmployeeModal model = new EmployeeModal();
            ViewBag.Role = DropDown.RoleDropdown();
            ViewBag.Country = DropDown.CountryDropdown();
            ViewBag.Gender = DropDown.genderDropdown();
            ViewBag.Bloodgroup = DropDown.BloodGroupDropdown();
            ViewBag.Qualification = DropDown.QualificationDropdown();
            ViewBag.DoctorCategory = DropDown.DoctorCategoryDropDown();
            if (id > 0)
            {
                model = EmployeeServices.UpdateEmployeeDetails(id);
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult EmployeeCreation(EmployeeModal obj)
        {
            if (ModelState.IsValid)
            {
                EmployeeServices acess = new EmployeeServices();

                if (obj.Id > 0)
                {

                    acess.updateEmployee(obj);
                }
                else
                {

                    acess.CreateEmployee(obj);
                }

            }
            return RedirectToAction("AdminDashboard");
        }

        public ActionResult DeleteEmployee(int id)
        {
            EmployeeServices obj = new EmployeeServices();
            obj.DeleteEmployee(id);
            return RedirectToAction("AdminDashboard", "Admin");
        }

        //public ActionResult Settings(int page = 1)
        //{

        //    int pageSize = 10;
        //    var data = new PagedData<DropDownCreation>();
        //    DropDownListModel model = new DropDownListModel();
        //    data = DropDown.DisplayDropDownMaster(page, pageSize);
        //    data.CurrentPage = page;
        //    data.pageSize = pageSize;
        //    data.visiblePages = 5;
        //    model.ResultField = data;
        //    return View(model);
        //}
        [HttpGet]
        public ActionResult _popupAddMasterDetails(int ID = 0)
        {
            DropDownCreation mdl = new DropDownCreation();
            if (ID > 0)
            {

            }
            return PartialView(mdl);
        }
        [HttpPost]
        public ActionResult _popupAddMasterDetails(DropDownCreation mdl)
        {
            DropDown obj = new DropDown();
            obj.createdropdown(mdl);

            return PartialView(mdl);
        }





        public ActionResult SettingsList()
        {
            DropDownListModel model = new DropDownListModel();
            PagedData<DropDownCreation> Data = new PagedData<DropDownCreation>();
            ViewBag.codename = DropDown.GetCodeNameList();
            try
            {
                int pageSize = 10;
                Data = DropDown.DisplayDropDownMaster(1, pageSize, string.Empty);
                Data.CurrentPage = 1;
                Data.pageSize = pageSize;
                Data.visiblePages = 5;
                model.ResultField = Data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(model);
        }

        public ActionResult SearchSettingsList(string CodeName, int page = 1)
        {
            var data = new PagedData<DropDownCreation>();
            DropDownListModel model = new DropDownListModel();
            try
            {
                int pageSize = 10;
                if (String.IsNullOrEmpty(CodeName))
                    CodeName = string.Empty;
                data = DropDown.DisplayDropDownMaster(page, pageSize, CodeName);
                data.CurrentPage = page;
                data.pageSize = pageSize;
                data.visiblePages = 5;
                model.ResultField = data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return PartialView(model);
        }

        public ActionResult EmployeeList()
        {
            EmployeeListModal model = new EmployeeListModal();
            var data = new PagedData<EmployeeModal>();
            ViewBag.Role = DropDown.RoleDropdown();
            try
            {
                int pageSize = 3;
                data = EmployeeServices.DisplayEmployeeListModel(1, pageSize, string.Empty);
                data.CurrentPage = 1;
                data.pageSize = pageSize;
                data.visiblePages = 3;
                model.ResultField = data;

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return View(model);
        }
        public ActionResult _SearchEmployeeList(int page = 1, string Role = "")
        {
            EmployeeListModal model = new EmployeeListModal();
            var data = new PagedData<EmployeeModal>();
            ViewBag.Role = DropDown.RoleDropdown();
            try
            {
                int pageSize = 3;
                data = EmployeeServices.DisplayEmployeeListModel(page, pageSize, Role);
                data.CurrentPage = page;
                data.pageSize = pageSize;
                data.visiblePages = 3;
                model.ResultField = data;

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return PartialView(model);
        }

        public ActionResult PatientList()
        {
            PatientListModel model = new PatientListModel();
            var data = new PagedData<Signup>();
            ViewBag.BloodGroup = DropDown.BloodGroupDropdown();

            try
            {
                int page = 1;
                int pageSize = 10;
                data = Signupservices.DisplayPatientList(page, pageSize, string.Empty);
                data.CurrentPage = page;
                data.pageSize = pageSize;
                data.visiblePages = 10;
                model.ResultField = data;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return View(model);
        }
        public ActionResult _SearchPatientList(int page, string BloodGroup = "")
        {
            PatientListModel model = new PatientListModel();
            var data = new PagedData<Signup>();
            ViewBag.BloodGroup = DropDown.BloodGroupDropdown();

            try
            {
                int pageSize = 10;
                data = Signupservices.DisplayPatientList(page, pageSize, BloodGroup);
                data.CurrentPage = page;
                data.pageSize = pageSize;
                data.visiblePages = 10;
                model.ResultField = data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return PartialView(model);
        }

        public ActionResult BasicPartialExamples()
        {
            return View();
        }
    }
}