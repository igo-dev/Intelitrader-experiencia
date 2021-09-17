using AutoMapper;
using Intelitrader_API.Dtos;
using Intelitrader_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelitrader_API.Data
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            CreateMap<GetUserDto, UserModel>();
            CreateMap<UserModel, GetUserDto>();

            CreateMap<CreateUserDto, UserModel>();
            CreateMap<UserModel, CreateUserDto>();

            CreateMap<UpdateUserDto, UserModel>();
            CreateMap<UserModel, UpdateUserDto>();
        }
    }
}
