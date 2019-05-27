using System;
using System.Collections.Generic;
using System.Linq;
using WarCardGame.Models;

namespace WarCardGame
{
    class Program
    {
        static void Main(string[] args)
        {

            var numberOfPlayers = getNumberOfPlayers();

            var deckOfCards = new CardDeck();
            deckOfCards.ShuffleDeck();

            var players = initializePlayers(numberOfPlayers).ToArray();

            var playerNumber = 0;
            var drawnCard = deckOfCards.DrawCard();
            while (drawnCard != null)
            {
                players[playerNumber].AddCard(drawnCard);
                drawnCard = deckOfCards.DrawCard();
                playerNumber++;

                if (playerNumber >= numberOfPlayers)
                {
                    playerNumber = 0;
                }
            }

            Console.ReadKey();
        }
        
        private static IEnumerable<Player> initializePlayers(int numberOfPlayers)
        {
            var players = new Player[numberOfPlayers];

            for(var index = 0; index < numberOfPlayers; index++)
            {
                players[index] = new Player();
            }

            return players;
        } 

        private static int getNumberOfPlayers()
        {
            var npcs = 0;
            while(npcs < 1)
            {
                Console.WriteLine("How many players?");

                var userInput = Console.ReadLine();

                int.TryParse(userInput, out npcs);

                if(npcs < 1)
                {
                    Console.WriteLine("Must have atleast 2 player to play.");
                }
            }
            return npcs;
        }
    }
}
