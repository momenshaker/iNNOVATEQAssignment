using App.Contract.Dto;
using App.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Helpers
{
    public class MapperProfile : Profile
    {
        /// <summary>
        /// To Map Dto and models 
        /// </summary>
        public MapperProfile()
        {
            CreateMap<Users, UserItemDto>()
                    .ForMember(x => x.FullAddress, a => a.MapFrom(x => (x.Country + ", " + x.State + ", " + x.Street + ", " + x.PinCode))).ReverseMap();
            CreateMap<Users, UserDto>().ReverseMap();
                

        }
    }
}
