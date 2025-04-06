using AutoMapper;
using ClassLibrary.Dto;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class MapProfile : Profile {
        public MapProfile() {

            CreateMap<Menu, MenuDto>();
        }
    }
}
