using AutoMapper;
using SearchAPI.Application.DTOs;
using SearchAPI.Domain.Entities;

namespace SearchAPI.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Rectangle, RectangleDto>().
                ReverseMap();
        }
    }
}
