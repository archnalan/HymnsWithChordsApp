using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text.RegularExpressions;

namespace HymnsWithChords.Data
{
	public class LyricExtractor
	{
		public static List<string> ExtractTxtFile(string filePath)
		{
			List<string> Lyrics = new List<string>();

			foreach (string line in File.ReadAllLines(filePath))
			{
				Lyrics.Add(line);
			}
			return Lyrics;
		}

		public static List<string> ExtractWordDocFile(string filePath)
		{
			List<string> Lyrics = new List<string>();
			using (WordprocessingDocument wordDoc = 
				WordprocessingDocument.Open(filePath, false))
			{
				//Get where the content lives
				var mainPart = wordDoc.MainDocumentPart;

				//Get all text from the document
				var paragraphs = mainPart.Document.Body.Descendants<Paragraph>();

				foreach (Paragraph paragraph in paragraphs)
				{
					Lyrics.Add(paragraph.ToString());
				}
				return Lyrics;
			}
		}
		public static List<string> ExtractPdfLyricFile(string pdfPath)
		{
			using (PdfReader reader = new PdfReader(pdfPath))
			
			using(PdfDocument document = new PdfDocument(reader)) 
			{
				List<string> Lyrics = new List<string>();
				ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

				for (int i = 1; i <= document.GetNumberOfPages(); i++)
				{
					string pageText = PdfTextExtractor
										.GetTextFromPage(document.GetPage(i), strategy);

					//split the text into Lines
					string[] lines = Regex.Split(pageText, 
									@"\r\n|\r|\n|(?:\r\n)+|(?:\r)+|(?:\n)+");

					foreach(string line in lines)
					{
						//Split lines into words and add to List
						/*var words = line.Split(new[] {' '}, 
							StringSplitOptions.RemoveEmptyEntries);

						Lyrics.AddRange(words);*/
						Lyrics.Add(line);
					}					

				}
				return Lyrics;
			}
			
		}
		
	}
}
