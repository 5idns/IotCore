using IotCore.ServiceDiscovery.Consul;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var configuration = app.Configuration;

ServiceEntity serviceEntity = new ServiceEntity
{
    IP = configuration["Service:IP"],
    Port = Convert.ToInt32(configuration["Service:Port"]),
    ServiceName = $"{app.Environment.EnvironmentName}/{configuration["Service:Name"]}",
    ConsulUrl = configuration["Consul:Url"],
    ConsulToken = configuration["Consul:Token"]
};
app.RegisterConsul(app.Lifetime, serviceEntity);

app.Run();
