using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
            MedicalRecords = new HashSet<MedicalRecord>();
            Referrals = new HashSet<Referral>();
        }

        public int DoctorId { get; set; }
        public string DoctorFirstName { get; set; } = null!;
        public string? DoctorMiddleName { get; set; }
        public string DoctorLastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? DoctorPhone { get; set; }
        public string DoctorEmail { get; set; } = null!;
        public int? HospitalId { get; set; }
        public string? SpecializationId { get; set; }
        public int? ClinicId { get; set; }

        public virtual Clinic? Clinic { get; set; }
        public virtual Hospital? Hospital { get; set; }
        public virtual Specialization? Specialization { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
        public virtual ICollection<Referral> Referrals { get; set; }
    }
}
