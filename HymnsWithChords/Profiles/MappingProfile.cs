﻿using AutoMapper;
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

            CreateMap<Chord, ChordEditDto>()                
                .ReverseMap()
                .ForMember(dest => dest.ChordCharts, opt=>opt.Ignore());

			CreateMap<Chord, ChordCreateDto>().ReverseMap();

			CreateMap<Chord, ChordWithChartsDto>()
                .ForMember(dest=>dest.Charts, opt=>opt.MapFrom(src=>src.ChordCharts))
                .ReverseMap();
            CreateMap<Chord, ChordWithChartsCreateDto>()
                .ForMember(dest=>dest.Charts, opt=>opt.MapFrom(src=>src.ChordCharts))
				.ReverseMap();
            CreateMap<Chord, ChordWithOneChartDto>()
                .ForMember(dest=>dest.ChordChart, opt=>opt.MapFrom(src=>src.ChordCharts))                
				.ReverseMap()
                .ForMember(dest=>dest.ChordCharts, opt=>opt.Ignore());//necessary for chart from Chord

            CreateMap<ChordChart, ChordChartCreateDto>().ReverseMap();
            CreateMap<ChordChart, ChordChartEditDto>().ReverseMap();
            CreateMap<ChordChart, ChartWithParentChordDto>()
                .ForMember(dest=>dest.ParentChord, opt=>opt.MapFrom(src=>src.Chord))
                .ReverseMap();
				

			CreateMap<LyricSegment,LyricSegmentDto>().ReverseMap();            
        }
    }
}
