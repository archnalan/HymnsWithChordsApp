using HymnsWithChords.Data;

namespace HymnsWithChords.Interfaces
{
	public class LyricHandlerFactory
	{
		public ILyricHandler GetLyricHandler(string filePath)
		{
			string fileExtension = Path.GetExtension(filePath).ToLowerInvariant();

			switch (fileExtension)
			{
				case ".txt":
					return new TxtLyricExtractor();
					
				case ".doc":
				case ".docx":
					return new WordDocExtractor();
					
				case ".pdf":
					return new PdfExtractor();
					
				default:
					throw new NotSupportedException("Unsupported file Type");
					
			}

		}
	}
}
