using AutoMapper;
using Hack_A_Thon.Server.src.API.Features.VideoGameManagement;
using Hack_A_Thon.Server.src.API.Infastructure;
using MediatR;

namespace Hack_A_Thon.Server.src.API.Common
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(VideoGameManagementController).Assembly);
            services.AddAutoMapper(typeof(VideoGameManagementController).Assembly);

            //var config = new MapperConfiguration(cfg =>
            //    cfg.AddProfile<VideoGameProfile>());
            //var mapper = config.CreateMapper();

            return services;
        }
    }
}
