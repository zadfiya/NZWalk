using AutoMapper;
using NZWalk.API.Model;
using NZWalk.API.Model.DTO;
using NZWalk.API.Model.DTO.Difficulty;
using NZWalk.API.Model.DTO.Walk;

namespace NZWalk.API.Mappings;

    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionDTO, Region>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<AddDifficultyDTO, Difficulty>().ReverseMap();
            CreateMap<UpdateDifficultyDTO, Difficulty>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<AddWalkDTO, WalkDTO>().ReverseMap();
            CreateMap<UpdateRegionDTO, WalkDTO>().ReverseMap(); 
        }
    }

