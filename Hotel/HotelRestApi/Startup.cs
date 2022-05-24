using HotelBusinessLogic.BusinessLogics;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelDatebaseImplement.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IOrganizerStorage, OrganizerStorage>();
            services.AddTransient<IConferenceStorage, ConferenceStorage>();
            services.AddTransient<ISeminarStorage, SeminarStorage>();
            services.AddTransient<IParticipantStorage, ParticipantStorage>();
            services.AddTransient<ILunchStorage, LunchStorage>();
            services.AddTransient<IRoomStorage, RoomStorage>();

            services.AddTransient<IOrganizerLogic, OrganizerLogic>();
            services.AddTransient<IConferenceLogic, ConferenceLogic>();
            services.AddTransient<ISeminarLogic, SeminarLogic>();
            services.AddTransient<IParticipantLogic, ParticipantLogic>();
            services.AddTransient<ILunchLogic, LunchLogic>();
            services.AddTransient<IRoomLogic, RoomLogic>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelRestApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelRestApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
