using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;
using EventCheckinSystem.Services.Services;
using EventCheckinSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



//builder.Services.AddIdentity<User, IdentityRole>()
//    .AddEntityFrameworkStores<EventCheckinManagementContext>()
//    .AddDefaultTokenProviders()
//    .AddDefaultUI();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddScoped<IEventServices, EventServices>();
builder.Services.AddScoped<IGuestCheckinServices, GuestCheckinServices>();
builder.Services.AddScoped<IGuestGroupServices, GuestGroupServices>();
builder.Services.AddScoped<IGuestImageServices, GuestImageServices>();
builder.Services.AddScoped<IGuestServices, GuestServices>();
builder.Services.AddScoped<IOrganizationServices, OrganizationServices>();
builder.Services.AddScoped<IWelcomeTemplateServices, WelcomeTemplateServices>();
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


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateIssuerSigningKey = true,
//        ValidateLifetime = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
//    };
//})
// .AddCookie("Identity.Bearer", options =>
// {
//     options.LoginPath = "/Account/Login";
//     options.AccessDeniedPath = "/Account/AccessDenied";
// });

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<EventCheckinManagementContext>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();
app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
