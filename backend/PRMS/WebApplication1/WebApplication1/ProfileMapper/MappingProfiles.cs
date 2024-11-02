using AutoMapper;
using PRMS_BackendAPI.DTO;
using PRMS_BackendAPI.DTO.AppointmentDTOs;
using PRMS_BackendAPI.DTO.CenteralCompany;
using PRMS_BackendAPI.Models;
using PRMS_BackendAPI.DTO;

namespace PRMS_BackendAPI.ProfileMapper
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {

            CreateMap<CenteralCompany, CenteralCompanyDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<Clinic, ClinicDTO>().ReverseMap();
            CreateMap<ClinicStaff, ClinicStaffDTO>().ReverseMap();
            CreateMap<Doctor, DoctorDTO>().ReverseMap();
            CreateMap<Hospital, HospitalDTO>().ReverseMap();
            CreateMap<HospitalStaff, HospitalStaffDTO>().ReverseMap();
            CreateMap<MedicalRecord, MedicalRecordDTO>().ReverseMap();
            CreateMap<Pateient, patientDTO>().ReverseMap();
            CreateMap<Referral, ReferralDTO>().ReverseMap();
            CreateMap<Specialization, SpecializationDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentDTODetails>().ReverseMap();
            CreateMap<CenteralCompany, CenteralCompanyNameDTO>().ReverseMap();
            CreateMap<CenteralCompany, ClinicDetaialCenteralCompanyDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
