using AutoMapper;
using HymnsWithChords.Dtos;
using HymnsWithChords.Dtos.CompositeDtos;
using HymnsWithChords.Dtos.WithUploads;
using HymnsWithChords.Models;
using HymnsWithChords.UI_Dtos;

namespace HymnsWithChords.Profiles
{
	public class MappingProfile:Profile
	{
        public MappingProfile()
        {
            CreateMap<Page, PageDto>().ReverseMap();

            CreateMap<HymnBook, HymnBookDto>().ReverseMap();
            CreateMap<HymnBook, HymnBookCreateDto>().ReverseMap();
            CreateMap<HymnBook, HymnBookWithCategoriesDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Hymn, HymnDto>()
                .ForMember(dest=>dest.CategoryName, src=>src.MapFrom(c=>c.Category.Name))
                .ReverseMap();
                
            CreateMap<Hymn, HymnCreateDto>().ReverseMap();

            CreateMap<Verse, VerseDto>()
                .ForMember(dest=>dest.HymnId, opt=>opt.MapFrom(v=>v.HymnId))                
                .ReverseMap()
				.ForMember(dest => dest.LyricLines, opt=>opt.Ignore())
                .ForMember(dest=>dest.Hymn, opt=>opt.Ignore());
            CreateMap<Verse, VerseCreateDto>().ReverseMap();

            CreateMap<LyricLine, LyricLineDto>().ReverseMap();
            CreateMap<LyricLine, LyricLineCreateDto>().ReverseMap();
            CreateMap<LyricLine, LineVerseCreateDto>().ReverseMap();

            CreateMap<LyricSegment, LyricSegmentDto>().ReverseMap();
            CreateMap<LyricSegment, LyricSegmentCreateDto>().ReverseMap();
            
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

            //WithUploads
            CreateMap<ChordChart, ChartCreateDto>().ReverseMap();
            CreateMap<ChordChart, ChartEditDto>().ReverseMap();
            CreateMap<ChordChart, ChartWithUploadsDto>().ReverseMap();

            ///////////////////////////////////////////////////////////////////////////////////////////

            CreateMap<Hymn, HymnChordsUIDto>().ReverseMap();
            CreateMap<Verse, VerseUIDto>().ReverseMap();
            CreateMap<LyricLine, LyricLineUIDto>().ReverseMap();
            CreateMap<LyricSegment, LyricSegmentUIDto>().ReverseMap();
            CreateMap<Chord, ChordUIDto>().ReverseMap();
            CreateMap<ChordChart, ChordChartUIDto>().ReverseMap();
        }
    }
}
