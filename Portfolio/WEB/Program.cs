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
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
