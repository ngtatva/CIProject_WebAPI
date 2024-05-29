using Business_logic_Layer;
using Data_Access_Layer.JWTService;
using Data_Access_Layer;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(db => db.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "localhost",
        ValidAudience = "localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddScoped<BALLogin>();
builder.Services.AddScoped<DALLogin>();
builder.Services.AddScoped<BALAdminUser>();
builder.Services.AddScoped<DALAdminUser>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

//INSERT INTO public."User"(
//    "Id", "FirstName", "LastName", "PhoneNumber", "EmailAddress", "UserType", "Password", "CreatedDate", "ModifiedDate", "IsDeleted")
//    VALUES(1, 'Nisarg', 'Gami', '9999999999', 'nisarg@tatvasoft.com', 'admin', 'nisarg@123', CURRENT_DATE, CURRENT_DATE, '0');

//INSERT INTO public."User"(
//    "Id", "FirstName", "LastName", "PhoneNumber", "EmailAddress", "UserType", "Password", "CreatedDate", "ModifiedDate", "IsDeleted")
//    VALUES(2, 'Nisarg', 'Gami', '9999999999', 'ngtestuser@tatvasoft.com', 'user', 'nisarg@123', CURRENT_DATE, CURRENT_DATE, '0');

//INSERT INTO public."UserDetail"(
//    "Id", "UserId", "Name", "Surname", "EmployeeId", "Manager", "Title", "Department", "MyProfile", "WhyIVolunteer", "CountryId", "CityId", "Avilability", "LinkdInUrl", "MySkills", "UserImage", "Status", "CreatedDate", "ModifiedDate", "IsDeleted")
//    VALUES(1, 2, 'Nisarg', 'Gami', 1, 'Manager', 'Demo', 'IT', 'abc', 'xyz', 4, 27, 'Standards Availability', '', '', '', true, CURRENT_DATE, CURRENT_DATE, '0');
