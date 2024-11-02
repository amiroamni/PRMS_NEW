namespace PRMS_BackendAPI.DTO
{
    public class CenteralCompanyDTO
    {
        public int SystemAdminId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CreatedDate { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public int? Status { get; set; }

        public int? ClinicId { get; set; }
        public int? HospitalId { get; set; }
    }
}
