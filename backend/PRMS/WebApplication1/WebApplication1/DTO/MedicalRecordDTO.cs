namespace PRMS_BackendAPI.DTO
{
    public class MedicalRecordDTO
    {
        public int MedicalRecordId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Medications { get; set; }
        public DateTime? DateMediaclRecord { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
    }
}
