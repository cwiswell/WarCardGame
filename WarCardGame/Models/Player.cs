using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WarCardGame.Infrastructure;

[assembly: InternalsVisibleTo("WarCardGame.Test")]
namespace WarCardGame.Models
{

    internal class Player
    {
        public bool ActivePlayer { get; private set; }

        private readonly IConsoleWrapper _consoleWrapper;
        private readonly Queue<Card> _hand;

        private bool isNpc { get; }
        private int playerNumber { get; }

        public Player(bool isNpc, int playerNumber, IConsoleWrapper consoleWrapper)
        {
            _consoleWrapper = consoleWrapper;

            ActivePlayer = true;
            this.isNpc = isNpc;
            this.playerNumber = playerNumber;
            _hand = new Queue<Card>();
        }

        public bool AnyCardsLeft()
        {
            return _hand.Any();
        }

        public void CheckIfStillActive()
        {
            if (ActivePlayer == true && !_hand.Any())
            {
                ActivePlayer = false;
            }
        }

        public Card DrawCard()
        {
            if (!ActivePlayer || !_hand.Any())
            {
                _consoleWrapper.WriteLine($"NPC {playerNumber} already out of game");
                return null;
            }
            var drawnCard = _hand.Dequeue();

            if (!isNpc)
            {
                _consoleWrapper.WriteLine($"You Drew: {drawnCard.ToString()}");
            }
            else
            {
                _consoleWrapper.WriteLine($"NPC {playerNumber} drew: {drawnCard.ToString()}");
            }

            return drawnCard;
        }

        public Card DrawHiddenCard()
        {
            if (!this.ActivePlayer || !_hand.Any()) return null;

            var drawnCard = _hand.Dequeue();

            return drawnCard;
        }

        public void AddCards(IList<Card> cards)
        {
            foreach (var card in cards.Where(x => x != null))
            {
                _hand.Enqueue(card);
            }
        }

        public void AddCard(Card card)
        {
            _hand.Enqueue(card);
        }

        public int GetHandCount()
        {
            return _hand.Count();
        }

        public string GetScore()
        {
            if (isNpc)
            {
                return $"NPC {playerNumber} - {GetHandCount()}";
            }
            else
            {
                return $"You - {GetHandCount()}";
            }
        }
    }
}
