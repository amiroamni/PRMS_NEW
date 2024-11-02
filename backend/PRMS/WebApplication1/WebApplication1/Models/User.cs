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
        public string? UserName { get; set; }

        public virtual Clinic? Clinic { get; set; }
        public virtual Hospital? Hospital { get; set; }
    }
}
