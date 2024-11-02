namespace PRMS_BackendAPI.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Password { get; set; } = null!;
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string Role { get; set; } = null!;
        public int? ClinicId { get; set; }
        public int? HospitalId { get; set; }
        public string? UserName { get; set; }

    }
}
