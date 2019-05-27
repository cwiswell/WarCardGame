using System;
using System.Collections.Generic;
using System.Linq;

namespace WarCardGame.Models
{
    internal class Player
    {
        private List<Card> _hand = new List<Card>();
        
        public bool AnyCardsLeft()
        {
            return _hand.Any();
        }

        public Card DrawCard()
        {
            var drawnCard = _hand.FirstOrDefault();

            if (drawnCard != null)
            {
                _hand.Remove(drawnCard);
            }

            return drawnCard;
        }

        public void AddCards(IList<Card> cards)
        {
            _hand.AddRange(cards);
        }

        public void AddCard(Card card)
        {
            _hand.Add(card);
        }
    }
}
