using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.CompilerServices;
using WarCardGame.Infrastructure;
using WarCardGame.Models;

[assembly: InternalsVisibleTo("WarCardGame.Test")]
namespace WarCardGame
{
    class Program
    {
        private static IServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to War Card Game");
            
            //Configure DI
            var serviceCollection = new ServiceCollection(); ;
            ConfigureProviders(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();

            askUserToPlay();
        }
                
        private static void ConfigureProviders(IServiceCollection services)
        {
            //add providers to service collection
            services.AddSingleton<IConsoleWrapper, ConsoleWrapper>()
                .AddTransient<IGameService, GameService>()
                .AddTransient<ICardDeck, CardDeck>();
        }

        private static void askUserToPlay()
        {
            Console.WriteLine("Would you like to play War? ");
            ConsoleYesNoText();

            var continuePlaying = userInputSwitch();
            while (continuePlaying)
            {
                Console.WriteLine("Would you like to play Again? ");
                ConsoleYesNoText();
                continuePlaying = userInputSwitch();
            }
        }

        private static bool userInputSwitch()
        {
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "y":
                case "Y":
                    Console.WriteLine("Good Luck!");
                    Console.WriteLine();
                    var game = serviceProvider.GetService<IGameService>();
                    game.StartGame();
                    return true;
                case "n":
                case "N":
                    return false;
                default:
                    Console.WriteLine($"{userInput} is not a valid answer");
                    Console.WriteLine("Try Again");
                    return userInputSwitch();
            }
        }

        private static void ConsoleYesNoText()
        {
            Console.WriteLine("[y] - yes    [n] - no");
        }
    }
}
