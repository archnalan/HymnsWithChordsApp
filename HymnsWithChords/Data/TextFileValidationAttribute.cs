using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace HymnsWithChords.Data
{
	public class TextFileValidationAttribute:ValidationAttribute
	{
		private readonly string[] _validateFileType;

        public TextFileValidationAttribute(params string[] validateFileType)
        {
			_validateFileType = validateFileType;
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;

			if (file == null) return new ValidationResult("File is required.");

			if (!IsValidFileType(file.FileName))
				return new ValidationResult("Invalid file content type. Only Plain text can be processed.");
			
			if(!IsValidContentType(file)) 
				return new ValidationResult($"Invalid File type. Only {string.Join(",",_validateFileType)} are allowed");

			if(file.ContentType == "application/msword"|| file.ContentType ==
				"application/vnd.openxmlformats-officedocument.wordprocessingml.document")
			{
				try
				{
					using (var memStream = new MemoryStream())
					{
						file.CopyTo(memStream);
						memStream.Position = 0;

						using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(memStream, false))
						{
							var mainPart = wordDoc.MainDocumentPart;

							var textElements = mainPart.Document.Descendants<Text>()
												.Where(t => string.IsNullOrWhiteSpace(t.Text));

							if (!textElements.Any()) return new ValidationResult("The document does not contain readable text");
						}
					}

				}catch(Exception ex)
				{
					return new ValidationResult($"An Error occured while Validating Word Document: {ex.Message}");
				}
			}

			if(file.ContentType == "application/pdf")
			{
				try
				{
					using(var memStream = new MemoryStream())
					{
						file.CopyTo(memStream);
						memStream.Position = 0;

						var pdfReader = new PdfReader(memStream);
						var pdfDocument = new PdfDocument(pdfReader);
						StringBuilder text = new StringBuilder();

						for(int i =1; i <= pdfDocument.GetNumberOfPages(); i++)
						{
							string pageContent = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i), 
							new SimpleTextExtractionStrategy());
							text.Append(pageContent);
						}

						if (string.IsNullOrWhiteSpace(text.ToString()))
						{
							return new ValidationResult("The pdf does not contain readable text");
						}							
					}
				}catch (Exception ex)
				{
					return new ValidationResult(
						$"An Error Occured while Validating the PDF: {ex.Message}");
				}
			}
			
			return ValidationResult.Success;
		}
		public bool IsValidFileType(string fileName)
		{
			return _validateFileType.Any(x => fileName.EndsWith(x, StringComparison.OrdinalIgnoreCase));
		}

		public bool IsValidContentType(IFormFile file)
		{
			return (file.ContentType == "text/plain" ||  //For .txt
				file.ContentType == "application/pdf"||  // For .pdf
				file.ContentType == "application/msword"|| //For .doc
				file.ContentType ==
				"application/vnd.openxmlformats-officedocument.wordprocessingml.document"); //For .docx
		}

		public static bool ValidateWordDocument(string filePath)
		{
			//Open the Word document
			using(WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
			{
				var mainPart = wordDoc.MainDocumentPart;

				var textElements = mainPart.Document.Descendants<Text>()
										   .Where(t => !string.IsNullOrWhiteSpace(t.Text));

				return textElements.Any();
			}
		}
    }
}
