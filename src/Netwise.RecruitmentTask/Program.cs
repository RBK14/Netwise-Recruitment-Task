using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Netwise.RecruitmentTask.Configuration;
using Netwise.RecruitmentTask.Execution;
using Netwise.RecruitmentTask.Execution.Abstractions;
using Netwise.RecruitmentTask.Services;
using Netwise.RecruitmentTask.Services.Abstractions;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<ApiSettings>(context.Configuration.GetSection(nameof(ApiSettings)));
        services.Configure<FileSettings>(context.Configuration.GetSection(nameof(FileSettings)));

        services.AddHttpClient<ICatFactClient, CatFactClient>();

        services.AddTransient<IFileWriter, FileWriter>();
        services.AddTransient<IProcessCatFactHandler, ProcessCatFactHandler>();
    })
    .Build();

var handler = host.Services.GetRequiredService<IProcessCatFactHandler>();

Console.WriteLine("=== Cat Fact Fetcher ===");
Console.WriteLine("Press [ENTER] to fetch a cat fact and write it to the output file.");
Console.WriteLine("Press [ESC]/[q]/[Q] to exit.");

while (true)
{
    var key = Console.ReadKey(intercept: true);

    if (key.Key == ConsoleKey.Escape || char.ToLower(key.KeyChar) == 'q')
    {
        Console.WriteLine("\nExiting...");
        break;
    }

    if (key.Key == ConsoleKey.Enter)
    {
        Console.WriteLine("\nFetching cat fact...");
        await handler.HandleAsync();
    } 
}