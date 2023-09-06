using System.Collections.Generic;
using System.Linq;
using Value;

namespace SeatsSuggestions;

public class Row : ValueType<Row>
{
    public Row(string name, List<SeatingPlace> seats)
    {
        Name = name;
        Seats = seats;
    }

    public string Name { get; init; }
    public List<SeatingPlace> Seats { get; init; }

    public Row AddSeat(SeatingPlace seatingPlace)
    {
        return new Row(Name, new List<SeatingPlace>(Seats) { seatingPlace });
    }

    public SeatingOptionSuggested SuggestSeatingOption(SuggestionRequest suggestionRequest)
    {
        var seatingOptionSuggested = new SeatingOptionSuggested(suggestionRequest);

        foreach (var seat in SelectAvailableSeatsCompliantWith(suggestionRequest.PricingCategory))
        {
            seatingOptionSuggested.AddSeat(seat);

            if (seatingOptionSuggested.MatchExpectation()) return seatingOptionSuggested;
        }

        return new SeatingOptionNotAvailable(suggestionRequest);
    }

    private IEnumerable<SeatingPlace> SelectAvailableSeatsCompliantWith(PricingCategory pricingCategory)
    {
        return Seats.Where(s => s.IsAvailable() && s.PricingCategory == pricingCategory);
    }

    public Row Allocate(SeatingPlace seatingPlace)
    {
        var newVersionOfSeats = new List<SeatingPlace>();

        foreach (var currentSeat in Seats)
            if (currentSeat.SameSeatLocation(seatingPlace))
                newVersionOfSeats.Add(new SeatingPlace(seatingPlace.RowName, seatingPlace.Number, seatingPlace.PricingCategory,
                    SeatAvailability.Allocated));
            else
                newVersionOfSeats.Add(currentSeat);

        return new Row(seatingPlace.RowName, newVersionOfSeats);
    }

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return new object[] { Name, new ListByValue<SeatingPlace>(Seats) };
    }
}