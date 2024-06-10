using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using HymnsWithChords.Data;
using HymnsWithChords.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("HymnData") ?? throw new InvalidOperationException("Connection string 'HymnData' not found.");
builder.Services.AddDbContext<HymnDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<HymnDbContext>();
builder.Services.AddControllersWithViews();

//Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddLogging();
//builder.Services.AddScoped<TextFileUploadService>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	try
	{
		var context = services.GetRequiredService<HymnDbContext>();
		context.Database.Migrate(); //Ensures migrations are applied
		CategoryData.Initialize(services);
	}
	catch(Exception ex)
	{		
		logger.LogError(ex, "An Error occured when while seeding data in the Database");
	}
}

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseMigrationsEndPoint();
	}
	else
	{
		app.UseExceptionHandler("/Home/Error");
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
