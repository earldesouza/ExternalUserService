using ExternalUserService.Options;
using ExternalUserService.Clients;
using ExternalUserService.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Bind ReqResApiOptions to the "ReqResApi" section in appsettings.json
        services.Configure<ReqResApiOptions>(context.Configuration.GetSection("ReqResApi"));

        // Register HttpClient for IReqResApiClient
        services.AddHttpClient<IReqResApiClient, ReqResApiClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<ReqResApiOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        // Register your ExternalUserService
        services.AddScoped<IExternalUserService, ExternalUserServiceImpl>();
    })
    .Build();

var userService = host.Services.GetRequiredService<IExternalUserService>();

var user = await userService.GetUserByIdAsync(2);

Console.WriteLine($"{user.FirstName} {user.LastName} - {user.Email}");