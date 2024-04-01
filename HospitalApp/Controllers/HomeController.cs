using HospitalApp.Models;
using HospitalApp.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
namespace HospitalApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModal su)
        {
            if (ModelState.IsValid)
            {
                var userstatus = LoginServices.Loginfun(su);
                var Role = LoginServices.RoleLogin(su);
                var id = IDServices.GetId(su);
                var EmpID = IDServices.GetEmpId(su);

                if (userstatus == true && Role == "Patient")
                {
                    Session["userID"] = id;
                    FormsAuthentication.SetAuthCookie(su.UserName, false);

                    //ViewBag.patientData = PatientServices.Patientdata(id);
                    return RedirectToAction("Dashboard", "Home");
                }
                else if (userstatus == true && Role == "Admin")
                {
                    FormsAuthentication.SetAuthCookie(su.UserName, false);
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else if (userstatus == true && Role == "Doctor")
                {
                    FormsAuthentication.SetAuthCookie(su.UserName, false);
                    return RedirectToAction("DoctorDashboard", "Doctor");
                }
                else
                {
                    TempData["msg"] = "incorrect username or password";
                    return View(su);

                }



            }


            return View(su);
        }

        public ActionResult Signup(int id = 0)
        {
            Signup model = new Signup();
            try
            {

                ViewBag.Country = DropDown.CountryDropdown();
                ViewBag.Gender = DropDown.genderDropdown();
                ViewBag.Bloodgroup = DropDown.BloodGroupDropdown();

                if (id > 0)
                {

                    model = Signupservices.updatePatientDetails(id);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Signup(Signup su)
        {
            Signupservices sgup = new Signupservices();
            EmployeeModal data = new EmployeeModal();

            if (ModelState.IsValid)
            {

                if ((su.patID > 0) && (su.Role == "Patient"))
                {
                    sgup.UpdatePatientDB(su);
                    return RedirectToAction("Dashboard", new { id = su.patID });

                }

                else if (su.patID > 0 && data.Role == "admin")
                {
                    sgup.UpdatePatientDB(su);
                    return RedirectToAction("AdminDashboard");

                }
                else
                {
                    sgup.getDetails(su);

                    return RedirectToAction("Dashboard", new { id = su.patID });

                }
            }

            return RedirectToAction("Login");
        }
        [Authorize]
        public ActionResult Dashboard()

        {
            try
            {
                if (Session["userid"] == null)
                {
                    RedirectToAction("Login");
                }
            }
            catch
            {
                RedirectToAction("Login");

            }
            return View();
        }
        public ActionResult DeletePatient(int id)
        {
            Signupservices obj = new Signupservices();
            obj.DeletePatient(id);
            return RedirectToAction("AdminDashboard");
        }
        [Authorize]
        public ActionResult AppointmentBooking()
        {
            
            ViewBag.DoctorCategory = DropDown.DoctorCategoryDropDown();
           
            return View();
        }
        [HttpPost]
        public ActionResult AppointmentBooking(AppointmentModel cr)
        {
            ViewBag.DoctorCategory = DropDown.DoctorCategoryDropDown();

            var patientID = IDServices.GetPatientId(User.Identity.Name);
            var appointmentstatus = EmployeeServices.bookingAppointment(cr,patientID);

            return RedirectToAction("Dashboard","Home");
        }

        public ActionResult GetDoctorsByCategory(string categoryId)
        {
            List<MasterModal> doctors = DropDown.DoctorByCategory(categoryId);
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSlotTimes(int doctorid, string date)
        {
        // Assuming SlotTime is a class representing slot time information
           List<MasterModal> slotTimes = DropDown.GetSlotTimesForDoctorAndDate(doctorid, date);

            
           return Json(slotTimes, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Logout()
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.Now.AddDays(-1d));
            // Response.Cache.SetNoStore();
            Response.Cookies.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult patientAppointmentBooking()
        {
            ViewBag.DoctorCategory = DropDown.DoctorCategoryDropDown();
            return View();
        }
        public ActionResult getAppointmentDetailsForPatient()
        {
            PatientAppointmentListModel model= new PatientAppointmentListModel();
            var data = new PagedData<AppointmentModel>();
            var patientID = IDServices.GetPatientId(User.Identity.Name);
            data = EmployeeServices.getAppointmentListForPatient(patientID);
            model.ResultField = data;
            return PartialView(model);
        }
        public ActionResult CancelAppointment(int? id)
        {
            if (id == null || id == 0)
            {
                
                return RedirectToAction("AppointmentBooking");
            }
            else
            {
                var cancel = EmployeeServices.cancelAppointmentForPatient(id.Value);
                return RedirectToAction("AppointmentBooking");
            }
        }




    }
}
   
