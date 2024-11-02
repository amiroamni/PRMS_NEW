using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class MedicalRecord
    {
        public int MedicalRecordId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Medications { get; set; }
        public DateTime? DateMediaclRecord { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }

        public virtual Doctor? Doctor { get; set; }
    }
}
