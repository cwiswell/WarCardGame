using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarCardGame.Models
{
    public class Game
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
            while (!isGameOver)
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
            printBoundary();
            var numOfPlayers = players.Count();
            var cardPot = new Card[numOfPlayers];

            cardPot[0] = players[0].DrawCard();

            for (var index = 1; index < numOfPlayers; index++)
            {
                cardPot[index] = players[index].DrawCard();
            }

            var maxCard = cardPot.Where(x=>x != null).Max(x => x);

            if (cardPot.Count(x => x == maxCard) > 1)
            {
                goToWar(maxCard, cardPot);
            }
            else
            {
                determineWinner(maxCard, cardPot);
            }
            printBoundary();
        }

        private void printBoundary()
        {
            Console.WriteLine(new string('-', 50));
        }

        private void goToWar(Card maxCard, Card[] cardPot)
        {
            var listOfWarPlayers = new List<int>();
            for(var index = 0; index < cardPot.Length; index++)
            {
                if(cardPot[index] == maxCard)
                {
                    listOfWarPlayers.Add(index);
                }
            }
            war(listOfWarPlayers, cardPot.Where(x => x != null).ToList());
        }

        private void war(List<int> playersInWar, List<Card> cardPot)
        {
            Console.WriteLine();
            Console.WriteLine("** WAR! **");
            var warCardPot = new Dictionary<int, Card>();

            foreach(var warPlayer in playersInWar.ToList())
            {
                var player = players[warPlayer];
                if(player.GetHandCount() < 2)
                {
                    while(player.AnyCardsLeft())
                    {
                        cardPot.Add(player.DrawCard());
                    }
                    playersInWar.Remove(warPlayer);
                    if(warPlayer == 0)
                    {
                        Console.WriteLine("You do not have enough cards to go to war and lose the conflict.");
                    }
                    else
                    {
                        Console.WriteLine($"NPC {warPlayer} does not have enough cards to go to war and lose the conflict.");
                    }
                }
                else
                {
                    cardPot.Add(player.DrawCard());
                    var drawnCard = player.DrawCard();
                    warCardPot.Add(warPlayer, drawnCard);
                    if (warPlayer != 0)
                    {
                        Console.WriteLine($"NPC {warPlayer} drew: {drawnCard.ToString()}");
                    }
                    else
                    {
                        Console.WriteLine($"You drew: {drawnCard.ToString()}");
                    }
                }
            }
            Console.WriteLine();
            var maxCard = warCardPot.Max(x => x.Value);

            cardPot.AddRange(warCardPot.Select(x => x.Value));

            if (warCardPot.Count(x=>x.Value == maxCard)== 1)
            {
                var winner = warCardPot.First(x => x.Value == maxCard);
                var winnerIndex = winner.Key;
                var winnerCard = winner.Value;

                var playerName = winnerIndex == 0 ? "You have" : $"NPC {winnerIndex} has";
                Console.WriteLine($"{playerName} won the War with {winnerCard.ToString()}");
                players[winnerIndex].AddCards(cardPot);
            }
            else
            {
                var playersStillInWar = warCardPot.Where(x => x.Value == maxCard).Select(x => x.Key).ToList();
                war(playersStillInWar,cardPot);
            }
        }

        private void determineWinner(Card maxCard, Card[] cardPot)
        {
            Console.WriteLine();
            for (var index = 0; index < cardPot.Length; index++)
            {
                if (cardPot[index] == maxCard)
                {
                    var playerName = index == 0 ? "You have" : $"NPC {index} has";
                    Console.WriteLine($"{playerName} won with {cardPot[index].ToString()}");
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
            if (!players[0].ActivePlayer)
            {
                Console.WriteLine("You have lost the game. Better luck next time.");
                return true;
            }

            if(players.Count(x => x.ActivePlayer) == 1)
            {
                displayWinnerText();

                return true;
            }
            else
            {
                return false;
            }       
        }

        private void displayWinnerText()
        {
            for(var index = 0; index < players.Count(); index++)
            {
                if (!players[index].ActivePlayer) continue;

                if (index != 0)
                {
                    Console.WriteLine($"NPC {index} has won the Game. Better luck next time.");
                }
                else
                {
                    Console.WriteLine("You have won the Game. Congratulations!");
                }
            }
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
            players[0] = new Player(false, 0);

            for (var index = 1; index < numberOfPlayers; index++)
            {                
                players[index] = new Player(true, index);
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
