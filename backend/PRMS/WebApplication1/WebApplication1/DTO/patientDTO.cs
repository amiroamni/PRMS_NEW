namespace PRMS_BackendAPI.DTO
{
    public class patientDTO
    {
        public int PatientId { get; set; }
        public string PatientFirstName { get; set; } = null!;
        public string? PatientMiddleName { get; set; }
        public string PatientLastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? PatientPhone { get; set; }
        public string? PatientEmailAddress { get; set; }
        public string? Address { get; set; }
        public int? MedicalRecordId { get; set; }
    }
}
