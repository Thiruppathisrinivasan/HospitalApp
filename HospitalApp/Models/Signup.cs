using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HospitalApp.Models
{
    public class Signup
    {

        public int patID { get; set; }
        public string Role { get; set; }
        [Required(ErrorMessage = "enter the name")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Name should be between 3 to 20 characters")]
        public string strName { get; set; }
        [Required(ErrorMessage = "enter the Gender")]
        public string bytGender { get; set; }
        [Required(ErrorMessage = "enter the age")]
        [Range(10, 95, ErrorMessage = "age should be 10 to 95")]
        public int intAge { get; set; }
        [Required(ErrorMessage = "enter the date of birth")]

        public DateTime dtDob { get; set; }
        [Required(ErrorMessage = "enter the blood group")]
        public string intBloodGroup { get; set; }
        [Required(ErrorMessage = "select the marital status")]

        public string bytMaritalStatus { get; set; }
        [Required(ErrorMessage = "enter the address")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Address should be between 5 to 50 characters")]
        public string strAddress { get; set; }
        [Required(ErrorMessage = "email is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Enter a valid email")]
        public string strEmail { get; set; }
        [Required(ErrorMessage = "Mobile number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Number should be 10 digits")]
        public string strContact { get; set; }
        [Required(ErrorMessage = "Nationality is required")]
        public string intNationalty { get; set; }
        [Required(ErrorMessage = "username is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username atleast contain 5 to 20 characters")]
        public string strUserName { get; set; }
        [Required(ErrorMessage = "password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "password atlest contain 5 to 20 characters")]
        public string strPassword { get; set; }
        [Required(ErrorMessage = "confirm password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "password atleast contain 5 to 20 characters")]
        [Compare("strPassword", ErrorMessage = "Password and Confirm Password do not match")]
        public string strConfirmPwd { get; set; }
    }

    public class PatientListModel
    {
        public string BloodGroup { get; set; }
        public PagedData<Signup> ResultField { get; set; }
    }


    public class AppointmentModel
    {
        [Required(ErrorMessage = "Date is required")]
        public string date { get; set; }
        [Required(ErrorMessage = "Time is required")]
        public int Time { get; set; }

        [Required(ErrorMessage = " please Select Category ")]
        public string Category { get; set; }
        [Required(ErrorMessage = " please Select Doctor ")]
        public int Doctor { get; set; }

        [Required(ErrorMessage = "Few words about symptoms")]
        public string Description { get; set; }

    }

    public class SlotTimeModal
    {    
        public int ID { get; set; }
        public string Time { get; set; }
    }

    public class PatientAppointmentListModel
    {
        
        public string Date { get; set; }
        public PagedData<AppointmentModel> ResultField { get; set; }


    }

}
