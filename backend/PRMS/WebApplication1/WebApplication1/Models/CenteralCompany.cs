using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class CenteralCompany
    {
        public int SystemAdminId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? CreatedDate { get; set; }
        public string? Phone { get; set; }
        public string? EmailAddress { get; set; }
        public int? ClinicId { get; set; }
        public int? HospitalId { get; set; }
        public int? Status { get; set; }

        public virtual Clinic? Clinic { get; set; }
        public virtual Hospital? Hospital { get; set; }
    }
}
