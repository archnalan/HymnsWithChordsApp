using HymnsWithChords.Controllers;
using HymnsWithChords.Data;
using HymnsWithChords.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HymnalUI
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize();
			var services = new ServiceCollection();

			services.AddSingleton<ILyricHandler, LyricExtractor>();
			services.AddSingleton<LyricExtractionController>();
			
			services.AddScoped<SdaHymnalUI>();			

			using (var service = services.BuildServiceProvider())
			{
				var mainForm = service.GetRequiredService<SdaHymnalUI>();
				// To customize application configuration such as set high DPI settings or default font,
				// see https://aka.ms/applicationconfiguration.
				
				Application.Run(mainForm);

			}
			
		}
		
	}
}