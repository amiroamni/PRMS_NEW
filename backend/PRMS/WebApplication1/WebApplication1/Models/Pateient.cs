using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class Pateient
    {
        public Pateient()
        {
            Appointments = new HashSet<Appointment>();
            Referrals = new HashSet<Referral>();
            Users = new HashSet<User>();
        }

        public int PatientId { get; set; }
        public string PatientFirstName { get; set; } = null!;
        public string? PatientMiddleName { get; set; }
        public string? PatientLastName { get; set; }
        public string? Gender { get; set; }
        public string? PatientPhoneNumeber { get; set; }
        public string? PatientEmailAddress { get; set; }
        public string? Address { get; set; }
        public int? MedicalRecordId { get; set; }
        public string? Age { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Referral> Referrals { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
