using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class Clinic
    {
        public Clinic()
        {
            CenteralCompanies = new HashSet<CenteralCompany>();
            ClinicStaffs = new HashSet<ClinicStaff>();
            Doctors = new HashSet<Doctor>();
            Users = new HashSet<User>();
        }

        public int ClinicId { get; set; }
        public string? ClinicName { get; set; }
        public string? ClinicType { get; set; }
        public string? ClinicPhone { get; set; }
        public string? ClinicEmail { get; set; }
        public string? ClinicLocation { get; set; }

        public virtual ICollection<CenteralCompany> CenteralCompanies { get; set; }
        public virtual ICollection<ClinicStaff> ClinicStaffs { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
