using AutoMapper;
using PM.Data.Entity;
using PM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PM.Buisness.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Plane, PlaneDTO>().ReverseMap();
            CreateMap<Plane, PlaneCreateDTO>().ReverseMap();
            CreateMap<Plane, PlaneUpdateDTO>().ReverseMap();

        }
    }
}
