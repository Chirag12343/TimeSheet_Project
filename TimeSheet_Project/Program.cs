using TimeSheet_Project.Models;

namespace TimeSheet_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
          
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<HolidayService>();
            //builder.Services.AddScoped<ITBL_EMPLOYEE, TBL_EMPLOYEE>();
            builder.Services.AddMemoryCache();


            // ? Add distributed memory cache
            builder.Services.AddDistributedMemoryCache();

            // ? Add session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularClient",
                    policy => policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    .AllowCredentials()
                );
            });
           
            var app = builder.Build();
        
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

           // app.UseCors("AllowReactApp");

            app.UseAuthorization();

            app.UseSession();
            app.UseCors("AllowAngularClient");
            app.MapControllers();
          

            app.Run();
        }
    }
}
