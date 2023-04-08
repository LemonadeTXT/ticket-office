using AutoMapper;
using TicketOffice.Mapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TicketOffice.BusinessLogic.Interfaces;
using TicketOffice.BusinessLogic.Services;
using TicketOffice.DAL;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllersWithViews();

var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(builder.Configuration["Logging:File:Path"].ToString(), rollingInterval:RollingInterval.Day).
    CreateLogger();
builder.Logging.AddSerilog(logger);

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Auth/Login");
                });

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperProfile());
});

var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
