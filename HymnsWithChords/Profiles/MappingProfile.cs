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
            CreateMap<Chord, ChordEditDto>().ReverseMap();
			CreateMap<Chord, ChordCreateDto>().ReverseMap();
			CreateMap<LyricSegment,LyricSegmentDto>().ReverseMap();
            CreateMap<ChordChart, ChordChartCreateDto>().ReverseMap();
            CreateMap<ChordChart, ChordChartEditDto>().ReverseMap();
        }
    }
}
