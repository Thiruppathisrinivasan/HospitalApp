//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppointmentBooking
    {
        public int bookingID { get; set; }
        public Nullable<int> patientID { get; set; }
        public Nullable<int> DoctorID { get; set; }
        public string Date { get; set; }
        public Nullable<int> Time { get; set; }
        public string Description { get; set; }
        public string status { get; set; }
    }
}
