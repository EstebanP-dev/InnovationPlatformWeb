using InnovationPlatform.Web.Client;
using InnovationPlatform.Web.Client.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("InnovationPlatform.Web.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("InnovationPlatform.Web.ServerAPI"));

builder.InstallServices(InnovationPlatform.Web.Shared.AssemblyReference.ExternalAssemblies);

builder.Services
    .AddFluentUIComponents();

await builder
    .Build()
    .RunAsync()
    .ConfigureAwait(false);
