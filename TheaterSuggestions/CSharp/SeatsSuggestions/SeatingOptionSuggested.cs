using System.Collections.Generic;

namespace SeatsSuggestions;

public class SeatingOptionSuggested
{
    public SeatingOptionSuggested(SuggestionRequest suggestionRequest)
    {
        PartyRequested = suggestionRequest.PartyRequested;
        PricingCategory = suggestionRequest.PricingCategory;
    }

    public PricingCategory PricingCategory { get; }
    public List<SeatingPlace> Seats { get; } = new();
    public int PartyRequested { get; }

    public void AddSeatingPlace(SeatingPlace seatingPlace)
    {
        Seats.Add(seatingPlace);
    }

    public bool MatchExpectation()
    {
        return Seats.Count == PartyRequested;
    }
}