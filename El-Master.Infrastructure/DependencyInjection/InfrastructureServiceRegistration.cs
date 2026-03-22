using El_Master.Application.Interfaces.Repositories;
using El_Master.Application.Interfaces.Services;
using El_Master.Domain.Entities;
using El_Master.Infrastructure.Presistence.Repositories;
using El_Master.Infrastructure.Presistence;
using El_Master.Infrastructure.ervices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using El_Master.Application.Settings;
using El_Master.Application.Interfaces;
using SqlKata.Compilers;
using SqlKata.Execution;
using El_Master.Infrastructure.Services;

namespace El_Master.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            // IDbConnection - Dapper
            services.AddScoped<IDbConnection>(sp =>
                            new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

            // SqlKata
            services.AddScoped<QueryFactory>(sp =>
            {
                var connection = sp.GetRequiredService<IDbConnection>();

                var compiler = new SqlServerCompiler();

                return new QueryFactory(connection, compiler);
            });

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            });

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ILessonAttachmentRepository, LessonAttachmentRepository>();
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IPackageLessonRepository, PackageLessonRepository>();
            services.AddScoped<ISubscriptionRepository,SubscriptionRepository>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IImageService, ImageService>(); 
            services.AddScoped<IFileStorageService, LocalFileStorageService>();

            // JWT Authentication
            services.Configure<JWT>(configuration.GetSection("JWT"));

            var jwtSettings = configuration.GetSection("JWT").Get<JWT>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key)),

                    ClockSkew = TimeSpan.Zero
                };
            });


            return services;
        }
    }
}
