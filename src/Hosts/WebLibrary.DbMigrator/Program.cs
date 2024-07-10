using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using WebLibrary.DbMigrator;

var host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
    {
        services.AddServices(hostContext.Configuration);
    }).Build();

await MigrateDatabaseAsync(host.Services);

await host.StartAsync();

static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MigrationDbContext>();

    await context.Database.MigrateAsync();
}