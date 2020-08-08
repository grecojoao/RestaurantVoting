using Voting.Domain.Handlers;
using Voting.Domain.Infra.Data;
using Voting.Domain.Infra.Repositories;
using Voting.Domain.Infra.Repositories.Contracts;
using Voting.Domain.Services;
using Voting.Domain.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Voting.Domain.Api
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
            services.AddControllers();
            services.AddTransient<IHungryProfessionalRepository, HungryProfessionalRepository>();
            services.AddTransient<IFavoriteRestaurantRepository, FavoriteRestaurantRepository>();
            services.AddTransient<IVoteRepository, VoteRepository>();
            services.AddTransient<IWinnerRestaurantRepository, WinnerRestaurantRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<RestaurantVotingHandler, RestaurantVotingHandler>();
            services.AddTransient<CreateFavoriteRestaurantHandler, CreateFavoriteRestaurantHandler>();
            services.AddSingleton<DataContextInMemory>();
            services.AddSingleton<IRestaurantVotingService, RestaurantVoting>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}