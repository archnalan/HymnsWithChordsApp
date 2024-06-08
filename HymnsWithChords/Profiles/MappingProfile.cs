using AutoMapper;
using HymnsWithChords.Dtos;
using HymnsWithChords.Models;

namespace HymnsWithChords.Profiles
{
	public class MappingProfile:Profile
	{
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Page, PageDto>().ReverseMap();
            CreateMap<Chord, ChordDto>().ReverseMap();
        }
    }
}
