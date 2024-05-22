using HymnsWithChords.Data;
using HymnsWithChords.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HymnsWithChords.Controllers
{
	public class LyricExtractionController : Controller
	{
		private readonly LyricHandlerFactory _lyricFactory;
		private readonly TextFileValidationAttribute _validator; 					

		public LyricExtractionController(LyricHandlerFactory lyricFactory)
        {
            _lyricFactory = lyricFactory;
			_validator= new TextFileValidationAttribute(".txt", ".doc", ".docx", ".pdf");
		}
        public async Task<List<string>> GetLyricsAsync(string filePath)
		{
			if ((_validator.IsValidFileType(filePath)) == false)
				throw new InvalidOperationException("Invalid File Type.");

			var lyricHandler = _lyricFactory.GetLyricHandler(filePath);
			return await lyricHandler.ExtractLyricsAsync(filePath);
		}
	}
}
