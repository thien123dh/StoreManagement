using Microsoft.EntityFrameworkCore;
using WarehouseManagementController.VnPayHelper;
using WarehouseManagementData.Models;
using WarehouseManagementRepository;
using WarehouseManagementRepository.Implement;
using WarehouseManagementRepository.Interface;
using WarehouseManagementService.Implement;
using WarehouseManagementService.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromMinutes(80);
    options.Cookie.HttpOnly = true; // Ensure the cookie is accessible only to the server.
    options.Cookie.IsEssential = true; // Make the session cookie essential.
});
builder.Services.AddDbContext<StoreManagementDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddTransient<Utils>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

app.Run();
