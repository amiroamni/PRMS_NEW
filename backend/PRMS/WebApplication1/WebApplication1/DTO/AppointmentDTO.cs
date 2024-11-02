namespace PRMS_BackendAPI.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? AppointmentDescription { get; set; }
        public string? AppointmentName { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? HospitalId { get; set; }

    }

}