using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class Specialization
    {
        public Specialization()
        {
            Doctors = new HashSet<Doctor>();
        }

        public string SpecializationId { get; set; } = null!;
        public string? SpecializationName { get; set; }
        public string? SpecializationDescription { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
