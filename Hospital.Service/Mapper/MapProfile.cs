using AutoMapper;
using Hospital.Core.DTOs;
using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AppointmentDto, Appointment>().ReverseMap();
            CreateMap<AppointmentCreateDto, Appointment>();
            CreateMap<AppointmentCreateDto, AppointmentDto>();
            CreateMap<AppointmentUpdateDto, Appointment>();

            CreateMap<PatientDto, Patient>().ReverseMap();
            CreateMap<PatientCreateDto, PatientDto>();
            CreateMap<PatientCreateDto, Patient>();
            CreateMap<PatientUpdateDto, Patient>();

            CreateMap<DoctorDto, Doctor>().ReverseMap();
            CreateMap<DoctorCreateDto, DoctorDto>();
            CreateMap<DoctorCreateDto, Doctor>();
            CreateMap<DoctorUpdateDto, Doctor>();
        }
    }
}
