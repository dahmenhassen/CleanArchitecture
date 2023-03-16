using API;
using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using IServiceScope scope = app.Services.CreateScope();
    ApplicationDbContextInitialiser initialiser =
        scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
    await initialiser.InitialiseAsync();
    await initialiser.SeedAsync();
    
    app.UseMiniProfiler();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi3(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller}/{action=Index}/{id?}");

app.Run();