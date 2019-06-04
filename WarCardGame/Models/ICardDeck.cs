
namespace WarCardGame.Models
{
    internal interface ICardDeck
    {
        void ShuffleDeck();
        bool AnyCardsLeft();
        ICard DrawCard();
    }
}
