using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class Referral
    {
        public int ReferralId { get; set; }
        public DateTime DateOfReferral { get; set; }
        public string ReferralDescription { get; set; } = null!;
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? HopsitalId { get; set; }
        public int? AppointmentId { get; set; }

        public virtual Appointment? Appointment { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual Hospital? Hopsital { get; set; }
        public virtual Pateient? Patient { get; set; }
    }
}
