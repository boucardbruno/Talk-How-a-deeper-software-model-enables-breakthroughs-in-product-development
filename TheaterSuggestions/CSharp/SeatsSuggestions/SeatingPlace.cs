using System.Collections.Generic;
using Value;

namespace SeatsSuggestions;

public class SeatingPlace : ValueType<SeatingPlace>
{
    public SeatingPlace(string rowName, int number, PricingCategory pricingCategory, SeatAvailability seatingPlaceAvailability)
    {
        RowName = rowName;
        Number = number;
        PricingCategory = pricingCategory;
        SeatingPlaceAvailability = seatingPlaceAvailability;
    }

    public string RowName { get; init; }
    public int Number { get; init; }
    public PricingCategory PricingCategory { get; init; }
    public SeatAvailability SeatingPlaceAvailability { get; init; }

    public bool IsAvailable()
    {
        return SeatingPlaceAvailability == SeatAvailability.Available;
    }

    public override string ToString()
    {
        return $"{RowName}{Number}";
    }

    public SeatingPlace Allocate()
    {
        if (SeatingPlaceAvailability == SeatAvailability.Available)
            return new SeatingPlace(RowName, Number, PricingCategory, SeatAvailability.Allocated);

        return this;
    }

    public bool SameSeatLocation(SeatingPlace seatingPlace)
    {
        return RowName == seatingPlace.RowName && Number == seatingPlace.Number;
    }

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return new object[] { RowName, Number, PricingCategory, SeatingPlaceAvailability };
    }
}