using Entities.Models;
using Entities.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace  StoreApp.Infrastructure.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDtoForInsertion,Product>();
        CreateMap<ProductDtoForUpdate,Product>().ReverseMap();
        CreateMap<UserDtoForCreation,IdentityUser>();
        CreateMap<UserDtoForUpdate,IdentityUser>().ReverseMap();
    }
}