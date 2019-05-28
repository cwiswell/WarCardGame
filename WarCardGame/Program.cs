using System;
using WarCardGame.Models;

namespace WarCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to War Card Game");

            askUserToPlay();
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
                    var game = new Game();
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
