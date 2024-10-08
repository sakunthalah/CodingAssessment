// See https://aka.ms/new-console-template for more information

using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using Microsoft.Extensions.DependencyInjection;
class Program
{
    static void Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<ITeamService, TeamService>();
        services.AddTransient<ITournamentService, TournamentService>();
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ITournamentService tournamentService = serviceProvider.GetRequiredService<ITournamentService>();

        StartApplication(tournamentService);

    }
    private static void StartApplication(ITournamentService tournamentService)
    {
        Console.WriteLine("Program Starting-------------");
        Console.WriteLine("");
        // feed the initial data
        tournamentService.SeedTournamentData();
   
        tournamentService.PathToVictory();
        tournamentService.GetTournamentWinner();
        Console.WriteLine("");
        Console.WriteLine("Program Completed-------------");
        Console.ReadLine();
    }
}