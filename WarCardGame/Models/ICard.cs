
namespace WarCardGame.Models
{
    internal interface ICard
    {
        CardSuiteEnum Type { get; set; }
        CardValueEnum Value { get; set; }
    }
}
