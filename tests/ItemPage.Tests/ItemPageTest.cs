using Bunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Xunit;
using eShop.WebApp.Components.Pages.Item; // Update with the correct namespace
using eShop.ServiceDefaults; // Update with the correct namespace
//using eShop.WebAppComponents.Services;
using eShop.WebAppComponents.Services;
//using eShop.WebAppComponents.Services.IProductImageUrlProvider;
public class ItemPageTest
{
    [Fact]
    public async Task WorkRequestComponent_RendersCorrectly()
    {
        using var context = new TestContext();

        // Register the CacheService or any other required services
        //context.AddServiceDefaults();

        context.Services.AddSingleton<CacheService>();
        context.Services.AddSingleton<EmailService>();

        //context.AddApplicationServices();

        // Render the component
        var component = context.RenderComponent<ItemPage>();

        // Get the component markup
        var markup = component.Markup;

        // Start the in-memory web server
        var host = CreateHost(markup);
        await host.StartAsync();

        // Open the markup in the default browser
        var url = "http://localhost:5000";
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });

        // Wait to allow time to view the page in the browser
        await Task.Delay(10000);

        // Stop the server
        // await host.StopAsync();
    }

    private IHost CreateHost(string markup)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    services.AddSingleton(markup);
                });

                webBuilder.Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapGet("/", async context =>
                        {
                            string? markup = context.RequestServices.GetService<string>();
                            context.Response.ContentType = "text/html";
                            //await context.Response.WriteAsync(markup ?? string.Empty);
                            //await context.Response.WriteAsync(markup);//, null, Encoding.UTF8);
                            await context.Response.WriteAsync($@"
                                <html>
                                <head>
                                    <style>{System.IO.File.ReadAllText("../../../ItemPage.razor.css")}</style>
                                </head>
                                <body>
                                    {markup}
                                </body>
                                </html>");
                                               
                        });
                    });
                });
            })
            .Build();
    }
}
