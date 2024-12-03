namespace PRMS_BackendAPI.DTO
{
    public class DoctorDTO
    {


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
        public string? HospitalName { get; set; }
        public string? ClinicName { get; set; }
    }
}
