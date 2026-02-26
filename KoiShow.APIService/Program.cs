using KoiShow.APIService.Helper;
using KoiShow.Data.Models;
using KoiShow.Service;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using KoiShow.APIService.Hubs;
using KoiShow.APIService.GrpcServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<FA24_SE171442_PRN231_AS_KoiShowContext>();
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});



builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the JWT token obtained from the login endpoint",
        Name = "Authorization"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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


// Configure CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(
//        name: MyAllowSpecificOrigins,
//        policy =>
//        {
//            policy.AllowAnyOrigin()   // Allow any origin
//                  .AllowAnyMethod()   // Allow any HTTP method (GET, POST, etc.)
//                  .AllowAnyHeader();  // Allow any headers (Authorization, etc.)
//        }
//    );
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins("http://127.0.0.1:5500") 
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); 
        });
});

// Configure Project DI
builder.Services.AddScoped<JwtHelper>();
builder.Services.AddScoped<ContestResultService>();
builder.Services.AddScoped<ContestService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<RegisterFormService>();
builder.Services.AddScoped<PointService>();
builder.Services.AddScoped<AnimalService>();
builder.Services.AddScoped<VarietyService>();

// Add authentication and JWT bearer configuration
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Add authorization
builder.Services.AddAuthorization();

// Add SignalR
builder.Services.AddSignalR();
builder.Services.AddGrpc();

var app = builder.Build();

// Automatically apply pending EF Core migrations on application startup
// This ensures the database is created and updated inside Docker container
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FA24_SE171442_PRN231_AS_KoiShowContext>();
    db.Database.Migrate(); // Apply any pending migrations
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<KoiHub>("/koiHub");

app.MapGrpcService<PointGrpcService>();

app.MapControllers();

app.Run();