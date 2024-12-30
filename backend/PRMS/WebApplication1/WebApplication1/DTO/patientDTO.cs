namespace PRMS_BackendAPI.DTO
{
    public class patientDTO
    {
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
    }
}
