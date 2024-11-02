using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;
using EventCheckinSystem.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using EventCheckinSystem.Repo.Configure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Repo.Repositories.Implements;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.WithOrigins("*")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "API"

    });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header sử dụng scheme Bearer.",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Name = "Authorization",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
            {
                new OpenApiSecurityScheme
                {
                Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
                }
            });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSingleton<TokenValidationParameters>(provider =>
{
    return new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        ValidIssuer = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ClockSkew = TimeSpan.FromMinutes(60)
    };
});
builder.Services.AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EventCheckinManagementContext>();

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
})
.AddEntityFrameworkStores<EventCheckinManagementContext>();

builder.Services.AddScoped<IEventRepo, EventRepo>();
builder.Services.AddScoped<IGuestCheckinRepo, GuestCheckinRepo>();
builder.Services.AddScoped<IGuestGroupRepo, GuestGroupRepo>();
builder.Services.AddScoped<IGuestImageRepo, GuestImageRepo>();
builder.Services.AddScoped<IGuestRepo, GuestRepo>();
builder.Services.AddScoped<IOrganizationRepo, OrganizationRepo>();
builder.Services.AddScoped<IWelcomeTemplateRepo, WelcomeTemplateRepo>();
builder.Services.AddScoped<IUserEventRepo, UserEventRepo>();
builder.Services.AddScoped<IAuthenticateRepo, AuthenticateRepo>();


builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddSingleton<IEmailService, EmailService>();   
builder.Services.AddScoped<ITimeService, TimeService>();
builder.Services.AddScoped<IEventServices, EventServices>();
builder.Services.AddScoped<IGuestCheckinServices, GuestCheckinServices>();
builder.Services.AddScoped<IGuestGroupServices, GuestGroupServices>();
builder.Services.AddScoped<IGuestImageServices, GuestImageServices>();
builder.Services.AddScoped<IGuestServices, GuestServices>();
builder.Services.AddScoped<IOrganizationServices, OrganizationServices>();
builder.Services.AddScoped<IWelcomeTemplateServices, WelcomeTemplateServices>();
builder.Services.AddScoped<IUserEventServices, UserEventServices>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddDbContext<EventCheckinManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<EventCheckinManagementContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
})
.AddCookie(options =>
{
    options.Cookie.Name = "jwt";
    options.Events = new CookieAuthenticationEvents
    {
        OnValidatePrincipal = async context =>
        {
            var token = context.Request.Cookies["jwt"];
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidateLifetime = true
            };

            var principal = handler.ValidateToken(token, validations, out var securityToken);
            context.Principal = principal;
        }
    };
});

//builder.Services.AddIdentityApiEndpoints<User>()
//    .AddEntityFrameworkStores<EventCheckinManagementContext>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()|| app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
