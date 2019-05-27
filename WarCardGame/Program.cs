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

            setUpPlayers(players, deckOfCards, numberOfPlayers);

            playGame(players, deckOfCards);

            Console.ReadKey();
        }
        
        private static void playGame(Player[] players, CardDeck deckOfCards)
        {
            while (true)
            {
                var isGameOver = checkForWinner(players);
                if (isGameOver) return;


            }
        }

        private static bool checkForWinner(Player[] players)
        {
            if (players.Any(x => !x.AnyCardsLeft()))
            {
                var winners = players.Where(x => !x.AnyCardsLeft());
                foreach (var winner in winners)
                {

                }
            }
            return false;
        }

        private static void setUpPlayers(Player[] players, CardDeck deckOfCards, int numberOfPlayers)
        {
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
