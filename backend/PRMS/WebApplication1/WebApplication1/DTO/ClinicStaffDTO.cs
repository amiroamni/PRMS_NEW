namespace PRMS_BackendAPI.DTO
{
    public class ClinicStaffDTO
    {
        public int ClinicStaffId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? ClinicPhone { get; set; }
        public string? EmailAddress { get; set; }
        public int? ClinicId { get; set; }

    }
}
