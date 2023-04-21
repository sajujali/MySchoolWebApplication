using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Data;
using Microsoft.AspNetCore.Identity;

using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Azure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using System.Configuration;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MySchool.Data;
using MySchool.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});
//builder.Services.AddDefaultIdentity<MySchoolUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<MySchoolContext>();

// Add services to the container.
builder.Services.AddRazorPages().AddMicrosoftIdentityUI();
builder.Services.AddDbContext<WebApplication1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplication1Context") ?? throw new InvalidOperationException("Connection string 'WebApplication1Context' not found.")));

/*builder.Services.AddDefaultIdentity<MySchoolUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MySchoolContext>();

/*builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration,"AzureAD");
builder.Services.AddMvc(option =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    option.Filters.Add(new AuthorizeFilter(policy));
  
}).AddMicrosoftIdentityUI();

builder.Services.AddSingleton<OpenIdConnectPostConfigureOptions>();*/



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
