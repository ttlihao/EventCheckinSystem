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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



//builder.Services.AddIdentity<User, IdentityRole>()
//    .AddEntityFrameworkStores<EventCheckinManagementContext>()
//    .AddDefaultTokenProviders()
//    .AddDefaultUI();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policyBuilder =>
    {
        policyBuilder.WithOrigins("https://your-frontend-domain.com")
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowCredentials();
    });
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddScoped<IEventRepo, EventRepo>();
builder.Services.AddScoped<IGuestCheckinRepo, GuestCheckinRepo>();
builder.Services.AddScoped<IGuestGroupRepo, GuestGroupRepo>();
builder.Services.AddScoped<IGuestImageRepo, GuestImageRepo>();
builder.Services.AddScoped<IGuestRepo, GuestRepo>();
builder.Services.AddScoped<IOrganizationRepo, OrganizationRepo>();
builder.Services.AddScoped<IWelcomeTemplateRepo, WelcomeTemplateRepo>();
builder.Services.AddScoped<IUserEventRepo, UserEventRepo>();
builder.Services.AddScoped<IAuthenticateRepo, AuthenticateRepo>();


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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    option.OperationFilter<SecurityRequirementsOperationFilter>();
});
//builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddDbContext<EventCheckinManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthorization();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
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
});

//builder.Services.AddIdentityApiEndpoints<User>()
//    .AddEntityFrameworkStores<EventCheckinManagementContext>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<EventCheckinManagementContext>()
.AddDefaultTokenProviders()
.AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<EventCheckinManagementContext>()
    .AddDefaultTokenProviders();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()|| app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.MapIdentityApi<User>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


app.MapControllers();

app.Run();
