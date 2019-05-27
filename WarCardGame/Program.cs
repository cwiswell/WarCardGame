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
            var numberOfPlayers = getNumberOfPlayers();

            var deckOfCards = new CardDeck();
            deckOfCards.ShuffleDeck();

            var players = initializePlayers(numberOfPlayers).ToArray();

            setUpPlayers(players, deckOfCards, numberOfPlayers);

            playGame(players);

            Console.ReadKey();
        }
        
        private static void playGame(Player[] players)
        {
            while (true)
            {
                var doContinue = consoleMenu(players);
                if (!doContinue) return;

                playHand(players);

                var isGameOver = checkForWinner(players);
                if (isGameOver) return;
            }
        }

        private static void playHand(Player[] players)
        {
            var numOfPlayers = players.Count();
            var cardPot = new Card[numOfPlayers];

            cardPot[0] = players[0].DrawCard();
            Console.WriteLine($"You Drew: {cardPot[0].Value.ToString()} of {cardPot[0].Type.ToString()}");

            for (var index = 1; index < numOfPlayers; index++)
            {
                cardPot[index] = players[index].DrawCard();

                Console.WriteLine($"NPC {index} drew: {cardPot[index].Value.ToString()} of {cardPot[index].Type.ToString()}");
            }

            var maxCard = cardPot.Max(x => x.Value);            

            if(cardPot.Count(x=> x.Value == maxCard) > 1)
            {
                war();
            }
            else
            {
                determineWinner(maxCard, cardPot, players);
            }
        }

        private static void war()
        {

        }

        private static void determineWinner(CardValueEnum maxValue, Card[] cardPot, Player[] players)
        {
            var loserList = new List<int>();

            for(var index = 0; index < cardPot.Length; index++)
            {
                if(cardPot[index].Value == maxValue)
                {
                    var playerName = index == 0 ? "You" : $"NPC {index}";
                    Console.WriteLine($"{playerName} has won with {cardPot[index].Value.ToString()} of {cardPot[index].Type.ToString()}.");
                }
                else
                {
                    loserList.Add(index);
                }
            }

            distributePotToLosers(players, loserList, cardPot);
        }

        private static void distributePotToLosers(Player[] players, List<int> losers, Card[] cardPot)
        {

        }

        private static bool consoleMenu(Player[] players)
        {
            while (true)
            {
                Console.WriteLine("[q] - quit    [c] - continue   [s] - scores ");
                var userEntry = Console.ReadLine();
                switch (userEntry)
                {
                    case "q":
                    case "Q":
                        Console.WriteLine("Quitting game");
                        return false;
                    case "s":
                    case "S":
                        Console.WriteLine(getPlayersCurrentDeckString(players));
                        break;
                    case "c":
                    case "C":
                        return true;
                    default:
                        Console.WriteLine($"{userEntry} is not a valid entry");
                        break;
                }
            }
        }

        private static string getPlayersCurrentDeckString(Player[] players)
        {
            var stringBuilder = new StringBuilder();
            for(var index = 0; index < players.Count(); index++)
            {
                if(index != 0)
                {
                    stringBuilder.AppendLine($"NPC {index} - {players[index].GetHandCount()}");
                }
                else
                {
                    stringBuilder.AppendLine($"You - {players[index].GetHandCount()}");
                }
            }

            return stringBuilder.ToString();
        }

        private static bool checkForWinner(Player[] players)
        {
            var listOfWinners = new List<int>();

            for(var index = 0; index < players.Count(); index++)
            {
                if (players[index].CheckIfWinner())
                {
                    listOfWinners.Add(index);
                }
            }
            var numberOfWinners = listOfWinners.Count();

            if(numberOfWinners == 0)
            {
                return false;
            }

            var containsPlayer = listOfWinners.Contains(0);

            if(numberOfWinners == 1 && containsPlayer)
            {
                Console.WriteLine("You have won!");
            }else if (containsPlayer)
            {
                Console.WriteLine($"You have tied with Computer player {String.Join(", ", listOfWinners.Where(x=> x!= 0).ToArray())}");
            }

            return true;
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
