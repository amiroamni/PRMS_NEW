using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class Hospital
    {
        public Hospital()
        {
            Appointments = new HashSet<Appointment>();
            CenteralCompanies = new HashSet<CenteralCompany>();
            Doctors = new HashSet<Doctor>();
            HospitalStaffs = new HashSet<HospitalStaff>();
            Referrals = new HashSet<Referral>();
            Users = new HashSet<User>();
        }

        public int HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public string? HospitalType { get; set; }
        public string? HospitalPhone { get; set; }
        public string? HospitalEmail { get; set; }
        public string? HospitalLocation { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<CenteralCompany> CenteralCompanies { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<HospitalStaff> HospitalStaffs { get; set; }
        public virtual ICollection<Referral> Referrals { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
