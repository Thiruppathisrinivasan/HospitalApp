using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace HospitalApp.Models
{
    public class DashboardModel
    {
        public PagedData<EmployeeModal> EmpList { get; set; }
        public List<Signup> PatList { get; set; }

        public DropDownCreation drop { get; set; }

    }

    public class EmployeeModal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "enter the EmployeeID")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "ID should be between 3 to 20 characters")]
        public string intEmpId { get; set; }
        [Required(ErrorMessage = "enter the role")]

        public string Role { get; set; }
        [Required(ErrorMessage = "enter the EmployeeName")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "name should be between 3 to 20 characters")]

        public string strEmpName { get; set; }
        [Required(ErrorMessage = "enter the Gender")]
        public string byGender { get; set; }
        [Required(ErrorMessage = "enter the date of birth")]
        public DateTime dtDob { get; set; }
        [Required(ErrorMessage = "enter the age")]
        [Range(22, 60, ErrorMessage = "age should be 22 to 60")]
        public int intAge { get; set; }
        [Required(ErrorMessage = "enter the blood group")]
        public string Bloodgrp { get; set; }
        [Required(ErrorMessage = "select the marital status")]
        public string byMaritalStatus { get; set; }
        [Required(ErrorMessage = "enter the qualification")]

        public string strQualification { get; set; }
        [Required(ErrorMessage = "select the specialization")]


        
        public string intSpecialization { get; set; }
        [Required(ErrorMessage = "enter the date of joining")]
        public string dtDoj { get; set; }
        [Required(ErrorMessage = "enter the address")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Address should be between 5 to 50 characters")]

        public string strAddress { get; set; }
        [Required(ErrorMessage = "email is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Enter a valid email")]

        public string strEmail { get; set; }
        [Required(ErrorMessage = "Mobile number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Number should be 10 digits")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Enter a valid phoneNumber")]
        public string strContactNo { get; set; }
        [Required(ErrorMessage = "Nationality is required")]

        public string intNationality { get; set; }
        [Required(ErrorMessage = "username is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username atleast contain 5 to 20 characters")]

        public string strUserName { get; set; }
        [Required(ErrorMessage = "password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "password atlest contain 5 to 20 characters")]

        public string strPassword { get; set; }
        [Required(ErrorMessage = "confirm password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "password atleast contain 5 to 20 characters")]
        [Compare("strPassword", ErrorMessage = "password and confirm password should match")]
        public string strConfirmPassword { get; set; }

    }

    public class PagedData<T> where T : class
    {
        public IEnumerable<T> data { get; set; }
        public int TotalPages { get; set; }   //total data to be loaded

        public int CurrentPage { get; set; }   //current page 
        public int pageSize { get; set; }      //how many data to be loaded in a page
        public int visiblePages { get; set; }   //
    }
    public class MasterModal
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    //    public class EmployeeSearchFieldModel{
    //        public string Role { get; set; }
    //        public string Qualification { get; set; } 
    //}
    public class EmployeeListModal
    {
        public string Role { get; set; }
        public PagedData<EmployeeModal> ResultField { get; set; }
    }

    public class DropDownCreation
    {
        public string CodeName { get; set; }
        public int ID { get; set; }
        public int codeId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

    }

    public class DropDownListModel
    {
        public string CodeName { get; set; }
        public PagedData<DropDownCreation> ResultField { get; set; }
    }



    public class DoctorAvailability
    {

        [Required(ErrorMessage = "Date is required")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Session is required")]

        public string Session { get; set; }
        [Required(ErrorMessage = "Time is required")]
        [Range(1, 3, ErrorMessage = "Timing between 1 to 3")]
        public int Time { get; set; }

        public List<DoctorAvailability> Shift { get; set; }
    }

    public class Availablityslotbooking
    {
        public int AvailabilityID { get; set; }
        public int doctorID { get; set; }
        public string Date { get; set; }
        public string session { get; set; }
        public string slotTime { get; set; }
        public int patientID { get; set; }
        public int scheduleID { get; set; }

        public string status { get; set; }
    }
    public class DoctorScheduleListModel
    {
        public string Session { get; set; }
        public string Date { get; set; }
        public PagedData<Availablityslotbooking> ResultField { get; set; }


    }

}