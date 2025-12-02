using AutoMapper;
using Entity;
using Manager.Services;
using Manager.Utility;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interfaces;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DB_Context --> Database 
builder.Services.AddDbContext<DB_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioDB")));

// Register generic repository [BaseRepo + IBaseRepo]
builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
builder.Services.AddScoped<IUserRepo, UserRepo>();

// Register AutoMapper
// IMapper object ( singleton by default )
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register BLL Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<EducationService>();
builder.Services.AddScoped<ExperienceService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/User/ServerError");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// For Error page ( wrong route ) --> middleware
app.UseStatusCodePagesWithReExecute("/Error/StatusCode", "?code={0}");

// These are middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");



// Get the AutoMapper instance from DI
var mapper = app.Services.GetRequiredService<IMapper>();
// Configure static MappingExtensions
MappingExtensions.Configure(mapper);

app.Run();
