using System.Collections.Generic;
using Value;

namespace SeatsSuggestions;

public class SeatingPlace : ValueType<SeatingPlace>
{
    public SeatingPlace(string rowName, int number, PricingCategory pricingCategory, SeatingPlaceAvailability seatingPlaceAvailability)
    {
        RowName = rowName;
        Number = number;
        PricingCategory = pricingCategory;
        SeatingPlaceAvailability = seatingPlaceAvailability;
    }

    public string RowName { get; }
    public int Number { get; }
    public PricingCategory PricingCategory { get; }
    public SeatingPlaceAvailability SeatingPlaceAvailability { get; }

    public bool IsAvailable()
    {
        return SeatingPlaceAvailability == SeatingPlaceAvailability.Available;
    }

    public override string ToString()
    {
        return $"{RowName}{Number}";
    }

    public SeatingPlace Allocate()
    {
        if (SeatingPlaceAvailability == SeatingPlaceAvailability.Available)
            return new SeatingPlace(RowName, Number, PricingCategory, SeatingPlaceAvailability.Allocated);

        return this;
    }

    public bool SameSeatingPlace(SeatingPlace seatingPlace)
    {
        return RowName == seatingPlace.RowName && Number == seatingPlace.Number;
    }

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return new object[] { RowName, Number, PricingCategory, SeatingPlaceAvailability };
    }

    public bool MatchCategory(PricingCategory pricingCategory)
    {
        if (PricingCategory.Mixed == pricingCategory)
        {
            return true;
        }
        return this.PricingCategory == pricingCategory;
    }
}