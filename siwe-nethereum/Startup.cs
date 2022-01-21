using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;

using FluentValidation;
using Nethereum.Metamask;
using Nethereum.Metamask.Blazor;
using Nethereum.UI;

using siwe_nethereum.Data;
using siwe_nethereum.RestServices;

namespace siwe_nethereum
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddSingleton<SiweRestService>(new SiweRestService("https://localhost:7148/"));
            services.AddSingleton<EnsMetadataService>(new EnsMetadataService());
            services.AddMudServices();

            services.AddScoped<IMetamaskInterop, MetamaskBlazorInterop>();
            services.AddScoped<MetamaskInterceptor>();
            services.AddScoped<MetamaskHostProvider>();
            services.AddScoped<IEthereumHostProvider>(serviceProvider =>
            {
                return serviceProvider.GetService<MetamaskHostProvider>();
            });

            services.AddScoped<IEthereumHostProvider, MetamaskHostProvider>();
            services.AddScoped<NethereumAuthenticator>();
            services.AddValidatorsFromAssemblyContaining<Nethereum.Erc20.Blazor.Erc20Transfer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            app.UseAuthentication();
        }
    }
}
