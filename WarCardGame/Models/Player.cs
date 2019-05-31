using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WarCardGame.Test")]
namespace WarCardGame.Models
{

    internal class Player
    {
        private Queue<Card> _hand = new Queue<Card>();
        public bool ActivePlayer { get; private set; }

        public Player()
        {
            ActivePlayer = true;
        }

        public bool AnyCardsLeft()
        {
            return _hand.Any();
        }

        public void CheckIfStillActive()
        {
            if(ActivePlayer == true && !_hand.Any())
            {
                ActivePlayer = false;
            }
        }

        public Card DrawCard()
        {
            if(!_hand.Any()) return null;
            var drawnCard = _hand.Dequeue();

            return drawnCard;
        }

        public void AddCards(IList<Card> cards)
        {
            foreach(var card in cards.Where(x=>x !=null))
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
