﻿using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Data
{
	public class FileExtensionValidationAttribute:ValidationAttribute
	{
		private readonly string[] _extensions;

        public FileExtensionValidationAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;

			if (file != null)
			{
				var extension = Path.GetExtension(file.FileName).ToLower();
				
				var isOkFileExtension = _extensions.Any(e=> extension.EndsWith(e));
				
				if (isOkFileExtension == false)
				{
					return new ValidationResult($"This file extension is not valid.");

				}				
			}			

			return ValidationResult.Success;
		}

	}
}
