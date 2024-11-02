using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class ClinicStaff
    {
        public int ClinicStaffId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? ClinicPhone { get; set; }
        public string? EmailAddress { get; set; }
        public int? ClinicId { get; set; }

        public virtual Clinic? Clinic { get; set; }
    }
}
