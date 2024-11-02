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
    }
}
