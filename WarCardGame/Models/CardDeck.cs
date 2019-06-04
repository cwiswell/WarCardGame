using System;
using System.Collections.Generic;
using System.Linq;
using WarCardGame.Infrastructure;

namespace WarCardGame.Models
{
    internal class CardDeck: ICardDeck
    {
        private readonly IList<ICard> _cards;

        public CardDeck()
        {
            _cards = new List<ICard>();
            foreach (var suite in (CardSuiteEnum[])Enum.GetValues(typeof(CardSuiteEnum)))
            {
                foreach (var cardValue in (CardValueEnum[])Enum.GetValues(typeof(CardValueEnum)))
                {
                    _cards.Add(new Card(suite, cardValue));
                }
            }
        }

        public void ShuffleDeck()
        {
            _cards.Shuffle();
        }

        public bool AnyCardsLeft()
        {
            return _cards.Any();
        }

        public ICard DrawCard()
        {
            var drawnCard = _cards.FirstOrDefault();

            if(drawnCard != null)
            {
                _cards.Remove(drawnCard);
            }

            return drawnCard;
        }
    }
}
