using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Persistence;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InnoGotchi.WebAPI;
using InnoGotchi.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IUsersDbContext).Assembly));
    config.AddProfile(new AssemblyMappingProfile(typeof(IFarmsDbContext).Assembly));
});
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

AuthOptions.KEY = builder.Configuration["KeyForJWT"];
builder.Services.AddApplication();
builder.Services.AddMvc(options => 
{
    options.EnableEndpointRouting = false;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = false,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
        option.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Query.ContainsKey("t"))
                {
                    context.Token = context.Request.Query["t"];
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAntiforgery();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<InnoGotchiDbContext>();
        DbInitializer.Initialize(context);
    }
    catch(Exception exeption)
    {

    }
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseMvc();
app.MapControllers();
app.Run();