namespace PRMS_BackendAPI.DTO.AppointmentDTOs;

public class AppointmentDTODetails
{



    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? AppointmentDescription { get; set; }
    public string? AppointmentName { get; set; }
    public string? Doctor_FirstName { get; set; }
    public string? Doctor_MiddleName { get; set; }
    public string? Doctor_LastName { get; set; }
    public string? Patient_FirstName { get; set; }
    public string? Patient_MiddleName { get; set; }
    public string? Patient_LastName { get; set; }
    public string? Hospital_Location { get; set; }
    public string? Hospital_Name { get; set; }





}
