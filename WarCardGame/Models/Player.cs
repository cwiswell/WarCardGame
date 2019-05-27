using System;
using System.Collections.Generic;
using System.Linq;

namespace WarCardGame.Models
{
    internal class Player
    {
        private Queue<Card> _hand = new Queue<Card>();
        internal int WintTotal { get; private set; }
        
        public bool AnyCardsLeft()
        {
            return _hand.Any();
        }

        public bool CheckIfWinner()
        {
            var isWinner = !_hand.Any();
            if (isWinner)
            {
                WintTotal++;
            }

            return isWinner;
        }

        public Card DrawCard()
        {
            if (!_hand.Any()) return null;
            var drawnCard = _hand.Dequeue();

            return drawnCard;
        }

        public void AddCards(IList<Card> cards)
        {
            foreach(var card in cards)
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
    }
}
