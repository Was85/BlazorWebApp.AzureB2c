using BlazorWebApp.AzureB2c.Client.Pages;
using BlazorWebApp.AzureB2c.Components;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddMicrosoftIdentityConsentHandler()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(options =>
    {
        builder.Configuration.Bind("AzureAdB2C", options);
        options.Events = new OpenIdConnectEvents
        {
            OnRedirectToIdentityProvider = async redirectContext =>
            {
                await Task.Yield();
            },
            OnAuthenticationFailed = async redirectContext =>
            {
                await Task.Yield();
            },
            OnSignedOutCallbackRedirect = async redirectContext =>
            {
                redirectContext.HttpContext.Response.Redirect(redirectContext.Options.SignedOutRedirectUri);
                redirectContext.HandleResponse();
                await Task.Yield();
            },
            OnTicketReceived = async redirectContext =>
            {
                if (redirectContext.Principal is { Identity: ClaimsIdentity identity })
                {
                    var colClaims = redirectContext.Principal.Claims.Select(claim => new { claim.Type, claim.Value }).ToList();
                    
                }
                await Task.Yield();
            },
            OnTokenValidated = async redirectContext =>
            {
                await Task.Yield();
            },
            OnAccessDenied = async redirectContext =>
            {
                await Task.Yield();
            },
            OnAuthorizationCodeReceived = async redirectContext =>
            {
                await Task.Yield();
            },
        };
    });
builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorWebApp.AzureB2c.Client._Imports).Assembly);

app.Run();
