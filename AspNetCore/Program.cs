using Catalog;
using Core;
using Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalog(builder.Configuration);

builder.Services.AddWebServices(builder.Configuration);

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var applicationDbContext = scopedProvider.GetRequiredService<CatalogDbContext>();
        await CatalogDbContextSeed.SeedAsync(applicationDbContext, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
