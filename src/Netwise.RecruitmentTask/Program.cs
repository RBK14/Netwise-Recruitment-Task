using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Netwise.RecruitmentTask.Configuration;
using Netwise.RecruitmentTask.Services;
using Netwise.RecruitmentTask.Services.Abstractions;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<ApiSettings>(context.Configuration.GetSection(nameof(ApiSettings)));
        services.Configure<FileSettings>(context.Configuration.GetSection(nameof(FileSettings)));

        services.AddHttpClient<ICatFactClient, CatFactClient>();
    })
    .Build();

await host.RunAsync();