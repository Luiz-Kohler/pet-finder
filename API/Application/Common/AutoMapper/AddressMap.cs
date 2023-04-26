﻿using Application.Services.User.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.AutoMapper
{
    public class AddressMap : Profile
    {
        public AddressMap()
        {
            CreateMap<CreateUserRequest, Address>()
                .ForMember(x => x.IsActive, opt => opt.MapFrom(x => true))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(x => DateTime.UtcNow));
        }
    }
}
