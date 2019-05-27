using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarCardGame.Models
{
    internal class Game
    {
        private Player[] players;

        public Game()
        {
            var numberOfPlayers = getNumberOfPlayers();
            var deckOfCards = new CardDeck();
            deckOfCards.ShuffleDeck();

            initializePlayers(numberOfPlayers);

            setUpPlayers(deckOfCards, numberOfPlayers);

        }
        public void StartGame()
        {
            playGame();
        }

        private void playGame()
        {
            while (true)
            {
                var doContinue = consoleMenu();
                if (!doContinue) return;

                playHand();

                var isGameOver = checkForWinner();
                if (isGameOver) return;
            }
        }

        private void playHand()
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

            if (cardPot.Count(x => x.Value == maxCard) > 1)
            {
                war();
            }
            else
            {
                determineWinner(maxCard, cardPot);
            }
            Console.WriteLine();
        }

        private void war()
        {
            Console.WriteLine("WAR!");
        }

        private void determineWinner(CardValueEnum maxValue, Card[] cardPot)
        {
            var loserList = new List<int>();

            for (var index = 0; index < cardPot.Length; index++)
            {
                if (cardPot[index].Value == maxValue)
                {
                    var playerName = index == 0 ? "You" : $"NPC {index}";
                    Console.WriteLine($"{playerName} has won with {cardPot[index].Value.ToString()} of {cardPot[index].Type.ToString()}.");
                }
                else
                {
                    loserList.Add(index);
                }
            }

            distributePotToLosers( loserList, cardPot);
        }

        private void distributePotToLosers(List<int> losers, Card[] cardPot)
        {
            var loser = losers.FirstOrDefault();
            foreach (var card in cardPot)
            {
                players[loser].AddCard(card);

                var nextLoserIndex = losers.IndexOf(loser) + 1;
                if (nextLoserIndex == losers.Count())
                {
                    nextLoserIndex = 0;
                }
                loser = losers[nextLoserIndex];
            }
        }

        private bool consoleMenu()
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
                        Console.WriteLine(getPlayersCurrentDeckString());
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

        private string getPlayersCurrentDeckString()
        {
            var stringBuilder = new StringBuilder();
            for (var index = 0; index < players.Count(); index++)
            {
                if (index != 0)
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

        private bool checkForWinner()
        {
            var listOfWinners = new List<int>();

            for (var index = 0; index < players.Count(); index++)
            {
                if (players[index].CheckIfWinner())
                {
                    listOfWinners.Add(index);
                }
            }
            var numberOfWinners = listOfWinners.Count();

            if (numberOfWinners == 0)
            {
                return false;
            }

            var containsPlayer = listOfWinners.Contains(0);

            if (numberOfWinners == 1 && containsPlayer)
            {
                Console.WriteLine("You have won!");
            }
            else if (containsPlayer)
            {
                Console.WriteLine($"You have tied with Computer player {String.Join(", ", listOfWinners.Where(x => x != 0).ToArray())}");
            }

            return true;
        }

        private void setUpPlayers(CardDeck deckOfCards, int numberOfPlayers)
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

        private void initializePlayers(int numberOfPlayers)
        {
            players = new Player[numberOfPlayers];

            for (var index = 0; index < numberOfPlayers; index++)
            {
                players[index] = new Player();
            }
        }

        private int getNumberOfPlayers()
        {
            var npcs = 0;
            while (npcs < 1)
            {
                Console.WriteLine("How many players?");

                var userInput = Console.ReadLine();

                int.TryParse(userInput, out npcs);

                if (npcs < 1)
                {
                    Console.WriteLine("Must have atleast 2 player to play.");
                }
            }
            return npcs;
        }
    }
}
