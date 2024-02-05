using Domain.Infrastructure;
using Domain.Interface;
using Domain.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWT Auth API",
        Version = "v1",
        Description = "API for JWT Authentication"
    });

    // Define the security scheme (JWT Bearer)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT token in the field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    // Use the Bearer scheme globally for all operations
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
         {
               new OpenApiSecurityScheme
                {
                     Reference = new OpenApiReference
                      {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                     }
               },
               Array.Empty<string>()
         }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtOptions:Key").Value ?? "");

      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = false,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key)
      };
  });


var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings");
var connectionString = mongoDBSettings["ConnectionString"];
var databaseName = mongoDBSettings["DatabaseName"];

builder.Services.AddSingleton(new MongoContext(connectionString, databaseName));

builder.Services.AddScoped<IShortLinkService, ShortLinkService>();

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
