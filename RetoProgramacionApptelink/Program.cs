using RetoProgramacionApptelink.Models.Data;
using RetoProgramacionApptelink.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(new Conexion(builder.Configuration.GetConnectionString("conexion")));
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IFamiliaProductoRepository, FamiliaProductoRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<ICabeceraFacturaRepository, CabeceraFacturaRepository>();
builder.Services.AddScoped<IDetalleFacturaRepository, DetalleFacturaRepository>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1); 
});
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}");

app.Run();
