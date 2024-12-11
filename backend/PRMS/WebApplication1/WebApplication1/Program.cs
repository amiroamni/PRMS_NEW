using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRMS_BackendAPI.Identity.Infra_Identitiy.Models;
using PRMS_BackendAPI.Identity.IdentityInterface;
using PRMS_BackendAPI.Identity.services;
using System.Text;
using Microsoft.AspNetCore.Identity;
using PRMS_BackendAPI.Identity.Infra_Identitiy;
using PRMS_BackendAPI.Identity.Identitys;
using PRMS_BackendAPI.Models;
using PRMS_BackendAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR();


// Configure DbContext for Identity
builder.Services.AddDbContext<PRMS_DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddDbContext<PRMS_IdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnectionIdentitiy")));

// Register Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<PRMS_IdentityDbContext>()
    .AddDefaultTokenProviders();

// Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.WithOrigins(
                    "http://localhost:4200",     // Angular HTTP
                    "https://localhost:4200",    // Angular HTTPS
                    "http://localhost:44386",    // Backend HTTP
                    "https://localhost:44386"    // Backend HTTPS
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed(origin => true) // This is more permissive for development
                .WithExposedHeaders("Content-Disposition"); // If you need to expose any headers
        });
});


// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:key"]))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthService, AuthService>();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PRMS Backend API", Version = "v1" });
});

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PRMS_BackendAPI.Api v1"));
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAllOrigins");
app.MapControllers();

app.UseEndpoints(endpoints =>
{


    endpoints.MapControllers();
    endpoints.MapHub<CommunicationHub>("/ChatHub");
});
//app.MapHub<CommunicationHub>("/chat-hub");
app.Run();
