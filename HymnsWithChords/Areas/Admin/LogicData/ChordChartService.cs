using AutoMapper;
using HymnsWithChords.Areas.Admin.Interfaces;
using HymnsWithChords.Data;
using HymnsWithChords.Dtos;
using HymnsWithChords.Dtos.WithUploads;
using HymnsWithChords.Models;
using HymnsWithChords.UI_Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HymnsWithChords.Areas.Admin.LogicData
{
	public class ChordChartService : IChordChartService
	{
		private readonly HymnDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHost;
		private readonly IHttpContextAccessor _contextAccessor;

		public ChordChartService(HymnDbContext context, 
			IMapper mapper, IWebHostEnvironment webHost, 
			IHttpContextAccessor contextAccessor)
		{
			_context = context;
			_mapper = mapper;
			_webHost = webHost;
			_contextAccessor = contextAccessor;
		}

		public async Task<List<ChartEditDto>> GetAllChordChartsAsync()
		{
			var chordCharts = await _context.ChordCharts
							.OrderBy(ch => ch.ChordId)
							.ToListAsync();

			var chordChartsDto = _mapper.Map<List<ChartEditDto>>(chordCharts);

			return chordChartsDto;
		}

		public async Task<(ChartWithUploadsDto, string)> GetChordChartByIdAsync(int id)
		{
			var chordChart = await _context.ChordCharts.FindAsync(id);

			if (chordChart == null) return (null, $"Chord with ID: {id} does not exist");

			var chordChartDto = _mapper.Map<ChartWithUploadsDto>(chordChart);

			string baseUrl = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}/lib/media/charts/";

			chordChartDto.FilePath = $"{baseUrl}{chordChartDto.FilePath}";

			if (!string.IsNullOrEmpty(chordChartDto.FilePath))
			{
				chordChartDto.ChartAudioFilePath = $"{baseUrl}{chordChartDto.ChartAudioFilePath}";
			}

			return (chordChartDto, null);
		}


		public async Task<(ChordChart, string)> GetChordChartWithChordByIdAsync(int id)
		{
			var chordChart = await _context.ChordCharts
				.Include(ct => ct.Chord)
				.FirstOrDefaultAsync(ct => ct.Id == id);

			return chordChart == null
				? (null, $"Chord with ID: {id} does not exist.")
				: (chordChart, null);
		}

		public async Task<(ChartEditDto, string)> CreateChordChartAsync(ChartCreateDto chartDto)
		{
			if (chartDto == null) return (null, "Chord Chart data is required.");			

			var chartExists = await _context.ChordCharts.Where(ch=>ch.FretPosition == chartDto.FretPosition)
							.AnyAsync(ch => ch.FilePath.EndsWith(chartDto.FilePath));

			if (chartExists) return (null, $"Chart with file path: {chartDto.FilePath} already exists at fret {chartDto.FretPosition}.");

			if (chartDto.ChordId != null)
			{
				var chordExists = await _context.Chords
					.AnyAsync(ch => ch.Id == chartDto.ChordId);
				if (!chordExists) return (null, $"Chord with ID:{chartDto.ChordId} does not exist.");
			}

			//Chart File Upload
			var upload = chartDto.ChartUpload;
			string imageName = "No-Image-Placeholder.svg.png";

			if(upload != null)
			{
				try
				{
					string uploadDir = Path.Combine(_webHost.WebRootPath, "lib/media/charts");

					if(Directory.Exists(uploadDir) == false)
					{
						Directory.CreateDirectory(uploadDir);
					}				

					imageName = Guid.NewGuid().ToString() + "_" + upload.FileName;

					string filePath = Path.Combine(uploadDir, imageName);

					using (FileStream fs = new FileStream(filePath, FileMode.Create))
					{
						await upload.CopyToAsync(fs);
					}
				}
				catch (Exception ex)
				{
					return(null, $"Error on chart upload: { ex.Message}");

				}
			}
			chartDto.FilePath = imageName;

			//Chart Audio File Upload
			var audioUpload = chartDto.ChartAudioUpload;

			if(!string.IsNullOrEmpty(chartDto.ChartAudioFilePath)) // empty string "" not considered 
			{
				var isAudioRepeat = await _context.ChordCharts
											.Where(ch => ch.FretPosition == chartDto.FretPosition)
											.AnyAsync(ch => ch.ChartAudioFilePath != null
											&& ch.ChartAudioFilePath.EndsWith(chartDto.ChartAudioFilePath));

				if (isAudioRepeat) return (null, $"Audio file path: {chartDto.ChartAudioFilePath} already in use at fret {chartDto.FretPosition}");
			}

			string audioFile = "Mute-Audio.wav";

			if(audioUpload != null)
			{
				try
				{
					string uploadDir = Path.Combine(_webHost.WebRootPath, "lib/media/charts/audio");

					if (Directory.Exists(uploadDir) == false)
					{
						Directory.CreateDirectory(uploadDir);
					}

					audioFile = Guid.NewGuid().ToString() + "_" + audioUpload.FileName;

					string filePath = Path.Combine(uploadDir, audioFile);

					using (FileStream fs = new FileStream(filePath, FileMode.Create))
					{
						await audioUpload.CopyToAsync(fs);
					}

				}
				catch(Exception ex)
				{
					return (null, $"Error on audio upload: {ex.Message}");
				}
			}

			chartDto.ChartAudioFilePath = audioFile;

			var chordChart = _mapper.Map<ChartCreateDto, ChordChart>(chartDto);

			try
			{
				_context.ChordCharts.Add(chordChart);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return (null, $"Error on saving chart: { ex.Message}");
			}

			var newChartDto = _mapper.Map<ChordChart, ChartEditDto>(chordChart);
			return (newChartDto, null);
		}

		public async Task<(ChartEditDto, string)> EditChordChartAsync(ChartEditDto chartEditDto)
		{
			if (chartEditDto == null) return (null, "Chord Chart data is required.");

			var chartInDb = await _context.ChordCharts.FindAsync(chartEditDto.Id);
			if (chartInDb == null) return (null, $"Chart with ID: {chartEditDto.Id} does not exist.");

			if (chartEditDto.ChordId != null)
			{
				var chordInDb = await _context.Chords.AnyAsync(ch => ch.Id == chartEditDto.ChordId);
				if (!chordInDb) return (null, $"Chord with ID: {chartEditDto.ChordId} does not exist.");
			}

			var chartExists = await _context.ChordCharts
				.Where(ch => ch.Id != chartEditDto.Id)
				.AnyAsync(ch => ch.FilePath.EndsWith(chartEditDto.FilePath) 
				&& ch.FretPosition == chartEditDto.FretPosition);

			if (chartExists) return (null, $"Chart with file path: {chartEditDto.FilePath} already exists at fret {chartEditDto.FretPosition}.");

			if (!string.IsNullOrEmpty(chartEditDto.ChartAudioFilePath))
			{
				//unique audio file paths per fret position
				var isRepeatAudio = await _context.ChordCharts
									.Where(ch => ch.Id != chartEditDto.Id)
									.AnyAsync(ch => !string.IsNullOrEmpty( ch.ChartAudioFilePath) && ch.ChartAudioFilePath.EndsWith(chartEditDto.ChartAudioFilePath)
									&& ch.FretPosition == chartEditDto.FretPosition);

				if (isRepeatAudio) return (null, $"Chart with audio path: {chartEditDto.FilePath} already in use at fret {chartEditDto.FretPosition}");

			}
			_mapper.Map(chartEditDto, chartInDb);
			try
			{				
				await _context.SaveChangesAsync();
				var updatedChartDto = _mapper.Map<ChordChart, ChartEditDto>(chartInDb);

				return (updatedChartDto, null);
			}
			catch (Exception ex)
			{
				return (null, ex.Message);
			}		

		}


		public async Task<(bool, string)> DeleteChordChartByIdAsync(int id)
		{
			var chordChart = await _context.ChordCharts.FindAsync(id);
			if (chordChart == null) return (false, $"Chart with ID: {id} does not exist.");

			try
			{
				_context.ChordCharts.Remove(chordChart);
				await _context.SaveChangesAsync();
				return (true, null);
			}
			catch (DbUpdateException ex)
			{
				if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 547)
					return (false, sqlEx.Message);

				return (false, ex.Message);
			}
			catch (Exception ex)
			{
				return (false, ex.Message);
			}
		}

		
	}
}
