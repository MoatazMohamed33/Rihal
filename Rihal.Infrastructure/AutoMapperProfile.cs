using AutoMapper;
using Rihal.Application.Dtos;
using Rihal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihal.Infrastructure
{
   public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, CreateStudentDto>().ReverseMap();
            CreateMap<Student, EditStudentDto>().ReverseMap();
            CreateMap<Student, StudentViewModel>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
            CreateMap<Country, CountryViewModel>().ReverseMap();
            CreateMap<Class, ClassViewModel>().ReverseMap();
        }
    }
}
