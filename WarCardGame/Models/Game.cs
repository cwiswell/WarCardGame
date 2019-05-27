using System;
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
            var isGameOver = false;
            while (isGameOver)
            {
                var doContinue = consoleMenu();
                if (!doContinue) return;

                playHand();

                checkForLosers();
                isGameOver = determineIsGameOver();
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
            for (var index = 0; index < cardPot.Length; index++)
            {
                if (cardPot[index].Value == maxValue)
                {
                    var playerName = index == 0 ? "You have" : $"NPC {index} has";
                    Console.WriteLine($"{playerName} won with {cardPot[index].Value.ToString()} of {cardPot[index].Type.ToString()}.");
                    players[index].AddCards(cardPot);
                    return;
                }
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

        private void checkForLosers()
        {
            foreach(var player in players.Where(x=> x.ActivePlayer))
            {
                player.CheckIfStillActive();
            }
        }

        private bool determineIsGameOver()
        {
            return players.Count(x => x.ActivePlayer) == 1;
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
