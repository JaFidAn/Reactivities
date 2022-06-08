using System.Runtime.CompilerServices;
using System.Net;
using API.Extensions;
using Application.Activities;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using FluentValidation.AspNetCore;
using API.Middleware;
using Microsoft.AspNetCore.Identity;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Application.Interfaces;
using API.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
})
.AddFluentValidation(config => 
{
    config.RegisterValidatorsFromAssemblyContaining<Create>();
});

// For Extensions methods
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();



//For Migration add this
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    //adding SeedData to database
    await Seed.SeedData(context, userManager);
}catch(Exception ex){
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseXContentTypeOptions();
app.UseReferrerPolicy(opt => opt.NoReferrer());
app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
app.UseXfo(opt => opt.Deny());
app.UseCsp(opt => opt 
    .BlockAllMixedContent()
    .StyleSources(s => s.Self().CustomSources(
                                            "https://fonts.googleapis.com", 
                                            "sha256-yChqzBduCCi4o4xdbXRXh4U/t1rP4UUUMJt+rB+ylUI=",
                                            "sha256-r3x6D0yBZdyG8FpooR5ZxcsLuwuJ+pSQ/80YzwXS5IU="
                                            ))
    .FontSources(s => s.Self().CustomSources(
                                            "https://fonts.gstatic.com", "data:"
                                            ))
    .FormActions(s => s.Self())
    .FrameAncestors(s => s.Self())
    .ImageSources(s => s.Self().CustomSources(
                                            "https://res.cloudinary.com", 
                                            "https://www.facebook.com",
                                            "https://platform-lookaside.fbsbx.com",
                                            "data:"
                                            ))
    .ScriptSources(s => s.Self().CustomSources(
                                            "sha256-7JP9bIEe1Ef92IdXbV/b1y4/vz2qKUJxCEWrwGZzLJY=",
                                            "https://connect.facebook.net", 
                                            "sha256-34O1GXEJR8GoBKPHtLqlMybCyfnmdxNWEuceG49gRzQ="
                                            ))
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.Use(async (context, next) => 
    {
        context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
        await next.Invoke();
    });
}

//app.UseHttpsRedirection();
app.UseRouting();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoint => 
{
    endpoint.MapControllers();
    endpoint.MapHub<ChatHub>("/chat");
    endpoint.MapFallbackToController("Index", "Fallback");
});

app.Run();
