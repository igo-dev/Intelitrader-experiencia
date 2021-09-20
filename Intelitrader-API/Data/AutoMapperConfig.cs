using AutoMapper;
using Intelitrader_API.Dtos;
using Intelitrader_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
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

            CreateMap<JsonPatchDocument<UpdateUserDto>, JsonPatchDocument<UserModel>>();
            CreateMap<Operation<UpdateUserDto>, Operation<UserModel>>();
        }
    }
}
