using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class HospitalStaff
    {
        public int HospitalStaffId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? HospitalStaffPhone { get; set; }
        public string? EmailAddress { get; set; }
        public int? HospitalId { get; set; }

        public virtual Hospital? Hospital { get; set; }
    }
}
