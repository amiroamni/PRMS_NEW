namespace PRMS_BackendAPI.DTO
{
    public class ReferralDTO
    {
        public int ReferralId { get; set; }
        public DateTime DateOfReferral { get; set; }
        public string ReferralDescription { get; set; } = null!;
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? HopsitalId { get; set; }
        public int? AppointmentId { get; set; }
        public int? ClinicId { get; set; }
        public string? Department { get; set; }
        public string? ClinicFindings { get; set; }
        public string? DiagnosisResult { get; set; }
        public string? Attachment { get; set; }
        public string? ReasonsForReferrals { get; set; }
        public string? RxGiven { get; set; }
        public string? Status { get; set; }
    }
}
