using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using MessageSenderAPI;
using MessageSenderAPI.AutoMapper;
using MessageSenderAPI.Domain.Helpers;
using MessageSenderAPI.Services.Interfaces;
using MessageSenderAPI.Services.Implementations;
using MessageSenderAPI.Services.Background;

var messageOrigins = "messageOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHostedService<EmailBackgroundService>();
builder.Services.AddHostedService<VerifiedUserBackgroundService>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAutoMapper(typeof(MappingProfile));

var frontUrl = builder.Configuration.GetSection("FrontURL").Value;
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(name: messageOrigins,
            policy =>
            {
                policy.WithOrigins(frontUrl)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    }
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(messageOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
