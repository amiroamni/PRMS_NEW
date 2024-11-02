using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            Referrals = new HashSet<Referral>();
        }

        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? AppointmentDescription { get; set; }
        public string? AppointmentName { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? HospitalId { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual Hospital? Hospital { get; set; }
        public virtual Pateient? Patient { get; set; }
        public virtual ICollection<Referral> Referrals { get; set; }
    }
}
