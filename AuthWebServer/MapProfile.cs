using AuthWebServer.ViewModel;
using AutoMapper;
using ClassLibrary.Dto;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthWebServer
{
    public class MapProfile : Profile {
        public MapProfile() {

            CreateMap<Menu, MenuDto>();
            CreateMap<MenuViewModel, Menu>();

            CreateMap<RoleViewModel, Role>();
            CreateMap<RoleViewModel, RoleDto>();
            CreateMap<RoleDto, RoleViewModel>();
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();
            CreateMap<User, UserDto>();
        }
    }
}
