
using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.UseCase;

namespace TheatricalPlayersRefactoringKata.Infrastructure.DI;

public static class DependencyInjectionConfig
{
    public static ServiceProvider Configure()
    {
        var services = new ServiceCollection();

        services.AddTransient<TragedyPlay>();
        services.AddTransient<ComedyPlay>();
        services.AddTransient<HistoricalPlay>();

        services.AddTransient<IPlayCalculator, TragedyPlay>(provider => new TragedyPlay());
        services.AddTransient<IPlayCalculator, ComedyPlay>(provider => new ComedyPlay());
        services.AddTransient<IPlayCalculator, HistoricalPlay>(provider => new HistoricalPlay());
        services.AddTransient<IXmlStatementPrinter, XmlStatementPrinter>();

        return services.BuildServiceProvider();
    }
}