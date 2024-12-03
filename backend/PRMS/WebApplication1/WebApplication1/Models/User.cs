using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Password { get; set; } = null!;
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string Role { get; set; } = null!;
        public int? ClinicId { get; set; }
        public int? HospitalId { get; set; }
        public string UserName { get; set; } = null!;
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public string? HopitalName { get; set; }
        public string? ClinicName { get; set; }

        public virtual Clinic? Clinic { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual Hospital? Hospital { get; set; }
        public virtual Pateient? Patient { get; set; }
    }
}
