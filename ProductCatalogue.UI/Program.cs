using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ProductCatalogue.UI.Services.Base;
using ProductCatalogue.UI.Services.Implementation;
using Blazored.Toast;
using Tewr.Blazor.FileReader;
using Radzen;
using Microsoft.AspNetCore.Components.Authorization;
using ProductCatalogue.UI.AuthProvider;
using Blazored.LocalStorage;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using MassTransit;
using ProductCatalogue.UI.Services.Helper;

namespace ProductCatalogue.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<ConfirmEmail>();
                x.UsingRabbitMq((content, cfg) =>
                {

                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    cfg.ReceiveEndpoint("usernotconfirmed", e =>
                    {

                        e.ConfigureConsumer<ConfirmEmail>(content);
                    });

                });
            });
            builder.Services.AddMassTransitHostedService();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44341/api/") });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddBlazoredToast();
            builder.Services.AddScoped<IAuthenticationHttpService, AuthenticationHttpService>();
            builder.Services.AddScoped<ICategoryHttpService, CategoryHttpService>();
            builder.Services.AddScoped<IProductHttpService, ProductHttpService>();
            builder.Services.AddScoped<IOfferHttpService, OfferHttpService>();
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<RefreshTokenService>();
            builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);
            await builder.Build().RunAsync();
        }
    }
}
