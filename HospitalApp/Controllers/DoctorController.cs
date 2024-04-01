using HospitalApp.Models;
using HospitalApp.services;
using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;

namespace HospitalApp.Controllers
{

    public class DoctorController : Controller
    {
        // GET: Doctor
        [Authorize]
        public ActionResult DoctorDashboard(int id=0)
        {
            DoctorAvailability model = new DoctorAvailability();
            ViewBag.MonthDta = DropDown.MonthDropdown();
            ViewBag.Sessiondta = DropDown.SessionDropDown();
            if(id > 0)             
                model.Shift = EmployeeServices.AvailabilityList();
            return View(model);
        }
        [Authorize]
        public ActionResult scheduleList()
        {

            var EmpID = IDServices.GetEmployeeId(User.Identity.Name);
            ViewBag.Sessiondta = DropDown.SessionDropDown();
            //ViewBag.SlotDate = DropDown.GetSlotDateForDoctor(EmpID);

            DoctorScheduleListModel model = new DoctorScheduleListModel();
            PagedData<Availablityslotbooking> Data = new PagedData<Availablityslotbooking>();
            try
            {
                int pageSize = 10;
               
                Data =EmployeeServices.displayslots(1,pageSize,EmpID,"","");
                Data.CurrentPage = 1;
                Data.pageSize = pageSize;
                Data.visiblePages = 10;
                model.ResultField = Data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(model);
        }
 
public ActionResult _searchScheduleList(int page=1,string date="", string session="")
        {
          

            ViewBag.Sessiondta = DropDown.SessionDropDown();
            DoctorScheduleListModel model = new DoctorScheduleListModel();
            PagedData<Availablityslotbooking> Data = new PagedData<Availablityslotbooking>();
            try
            {
                int pageSize = 10;
                var EmpID = IDServices.GetEmployeeId(User.Identity.Name);
                Data = EmployeeServices.displayslots(page, pageSize,EmpID,date,session);
                Data.CurrentPage = 1;
                Data.pageSize = pageSize;
                Data.visiblePages = 10;
                model.ResultField = Data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return PartialView(model);
        }    
        
        public ActionResult Delete(int id)
        {
            var delstatus = EmployeeServices.deleteschedule(id);

            
            return RedirectToAction( "scheduleList", "Doctor");
        }

        public ActionResult Leave(int id)
        {
            var leavestatus = EmployeeServices.Leaveschedule(id);
            if (leavestatus == 1)
            {
                TempData["msg"] = "Waiting for transfer";
            }
            return RedirectToAction("scheduleList", "Doctor");
        }
        public ActionResult _DoctorAvailablityBooking()
        {
            //ViewBag.MonthDta = DropDown.MonthDropdown();
            ViewBag.Sessiondta = DropDown.SessionDropDown();
            return PartialView();
        }
        [HttpPost]
        public ActionResult _DoctorAvailablityBooking(DoctorAvailability su)
        {
            // ViewBag.MonthDta = DropDown.MonthDropdown();
            ViewBag.Sessiondta = DropDown.SessionDropDown();

            int slottime = Convert.ToInt32(WebConfigurationManager.AppSettings["SlotTime"]);


            var EmpID = IDServices.GetEmployeeId(User.Identity.Name);
            //var creation = EmployeeServices.DoctorScheduleAvailability(su, EmpID, slottime);
            var creation2 = EmployeeServices.DoctorScheduleAvailability1(su, EmpID, slottime);

            if (creation2 == true)
            {
                TempData["msg"] = "schedule added sucessfully";
                return RedirectToAction("DoctorDashboard", "Doctor");
            }
            else
            {
                TempData["msg"] = "schedule not added please try again";
            }


            return RedirectToAction("DoctorDashboard", "Doctor");
        }

    public ActionResult doctorAvailabilityList()
        {
            DoctorScheduleListModel model = new DoctorScheduleListModel();
            PagedData<Availablityslotbooking> Data = new PagedData<Availablityslotbooking>();
            var EmpID = IDServices.GetEmployeeId(User.Identity.Name);
            Data = EmployeeServices.getDoctorAvailability(EmpID);
            model.ResultField = Data;
                
               return PartialView(model);
        }
   

    }


}