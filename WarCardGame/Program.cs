using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            userInputSwitch();
            
            while (true)
            {
                Console.WriteLine("Would you like to play Again? ");
                ConsoleYesNoText();
                userInputSwitch();
            }
        }

        private static void userInputSwitch()
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
                    break;
                case "n":
                case "N":
                    break;
                default:
                    Console.WriteLine($"{userInput} is not a valid answer");
                    Console.WriteLine("Try Again");
                    userInputSwitch();
                    break;
            }
        }

        private static void ConsoleYesNoText()
        {
            Console.WriteLine("[y] - yes    [n] - no");
        }
    }
}
