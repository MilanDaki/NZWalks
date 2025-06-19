using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();

            CreateMap<AddWalkRequestDto, Walks>().ReverseMap();
            CreateMap<Walks, WalkDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walks>().ReverseMap();


        }
    }
}
