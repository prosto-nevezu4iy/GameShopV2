using AspNetCore.Configuration;
using AspNetCore.Middlewares;
using BasketProject;
using Catalog;
using OrderProject;
using System.IdentityModel.Tokens.Jwt;
using Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalog(builder.Configuration);
builder.Services.AddBasket(builder.Configuration);
builder.Services.AddOrder(builder.Configuration);

builder.Services.AddWebServices(builder.Configuration);

builder.Services.AddControllersWithViews();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

var identityConfig = builder.Configuration.GetSection("Identity").Get<Identity>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = identityConfig.Authority;

        options.ClientId = identityConfig.ClientId;
        options.ClientSecret = identityConfig.ClientSecret;
        options.ResponseType = "code";

        options.Scope.Add("profile");
        options.GetClaimsFromUserInfoEndpoint = true;

        options.SaveTokens = true;
    });

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

app.UseTransferAnonymousBasketToUser();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
