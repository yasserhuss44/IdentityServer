using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace IdentityServerClient
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
            services.AddCors(o => o.AddPolicy("NUXT", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
            IdentityModelEventSource.ShowPII = true; //Add this line

            services.AddControllers();
            //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    // options.Authority = "https://localhost:44318";
                    // options.ClientId = "mvc3";
                    // options.ClientSecret = "secret";
                    // options.ResponseType = "code";
                    // options.SaveTokens = true;


                    options.Authority = "https://dev-ysallam.us.auth0.com";
                    options.ClientId = "FSseCg0osWEc4J2yZw6a03F6fPMaZQ0Y";
                    options.ClientSecret = "6hSSD5ZnDb-Bd2hVN3wGpxJsQhuSWKvwEHgZIIM58IUJ4gTY5qOgqWNogHGhUqDC";
                    options.ResponseType = "code";
                    options.SaveTokens = true;
                    // options.CallbackPath = "/WeatherForecast";

                    //   authority: 'https://dev-ysallam.us.auth0.com',
                    //   redirectUrl: window.location.origin + '/callback1',
                    //   postLogoutRedirectUri: window.location.origin,
                    //   clientId: 'Qppoms0Iehi73UQzDRS824RP7u8QNm7U',
                    //   scope: 'openid profile email offline_access',
                    //   responseType: 'code',
                });

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

app.UseCors("NUXT");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
