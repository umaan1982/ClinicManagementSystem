using ClinicManagementSystem.Configurations;
using ClinicManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    builder.Services.AddRazorPages();
    LoadConfiguration(builder);
    InitiateLogger();
    AddDbContext(builder);
    WebApplication app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();
    app.MapRazorPages();
    app.Run();
}
catch(Exception ex)
{
    Console.WriteLine("System Failed to start. Error:" + ex.Message);
}

void LoadConfiguration(WebApplicationBuilder builder)
{
    try
    {
        IServiceCollection services = builder.Services;
        ConfigurationManager configuration = builder.Configuration;
        AppSettings.IntializeConfiguration(configuration);
    }
    catch ( Exception ex)
    {
        throw new Exception("Failed to load configuration. See details here: " + ex.Message);
    }
}

void InitiateLogger()
{
    try
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    }
    catch (Exception ex)
    {
        throw new Exception("Failed to load Logger. Please see details: " + ex.Message);
    }
}

void AddDbContext(WebApplicationBuilder builder)
{
    try
    {
        builder.Services.AddDbContext<DBContext>(options => options.UseNpgsql(AppSettings.PostGreConnectionString));
    }
    catch(Exception ex)
    {
        Log.Error("Error in DbContext. See Error: " + ex.Message);
    }
}

