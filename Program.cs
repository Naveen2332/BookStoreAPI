using BookStore.Data;
using BookStore.Model;
using BookStore.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IBookRepo, BookRepo>();
builder.Services.AddTransient<IAccountRepo, AccountRepo>();


builder.Services.AddLogging(builder => builder.AddConsole() );
builder.Services.AddDbContext<BookStoreContext>
(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreContext")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BookStoreContext>()
    .AddDefaultTokenProviders();

//---------------{
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(options =>
   {
       options.SaveToken = true;
       options.RequireHttpsMetadata = false;
       options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidAudience = builder.Configuration["JWT:validAudience"],
           ValidIssuer = builder.Configuration["JWT:validIssuer"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))

       };
   });
//------------------}



builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddAutoMapper(typeof(Program));//----{}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//--------------------------------------------{
builder.Services.AddCors(options =>
{
    options.AddPolicy("policyDefault", build =>
    {
        //builder.WithOrigins("http://localhost:","http://localhost:4200" /*or*/ "*" ).AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
//--------------------------------------------}


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("policyDefault");//----{}

app.UseHttpsRedirection();

app.UseAuthentication();//----{}

app.UseAuthorization();

app.MapControllers();

app.Run();
