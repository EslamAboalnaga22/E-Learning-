using ELearning.Core.Dtos;
using ELearning.Core.Entities;
using ELearning.Core.InterfacesClient;
using ELearning.Infrastructure.RepositoriesClient;

//using ELearning.Infrastructure.RepositoriesClient;
using ELearningWASM;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddWsamDI(builder.Configuration);

await builder.Build().RunAsync();

////password --> P@ssword123
