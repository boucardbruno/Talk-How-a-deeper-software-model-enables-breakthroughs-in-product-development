﻿using System.Collections.Generic;

namespace SeatsSuggestions;

public class SeatingPlaceSuggestingMaker
{
    private const int NumberOfSuggestionsPerPricingCategory = 3;
    private readonly AuditoriumSeatingAdapter _auditoriumSeatingAdapter;

    public SeatingPlaceSuggestingMaker(AuditoriumSeatingAdapter auditoriumSeatingAdapter)
    {
        _auditoriumSeatingAdapter = auditoriumSeatingAdapter;
    }

    public SuggestionsMade MakeSuggestions(string showId, int partyRequested)
    {
        var auditoriumSeating = _auditoriumSeatingAdapter.GetAuditoriumSeating(showId);

        var suggestionsMade = new SuggestionsMade(showId, partyRequested);

        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeating, partyRequested, PricingCategory.First));
        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeating, partyRequested, PricingCategory.Second));
        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeating, partyRequested, PricingCategory.Third));

        if (suggestionsMade.MatchExpectations()) return suggestionsMade;

        return new SuggestionNotAvailable(showId, partyRequested);
    }

    private static IEnumerable<SuggestionMade> GiveMeSuggestionsFor(
        AuditoriumSeating auditoriumSeating,
        int partyRequested,
        PricingCategory pricingCategory)
    {
        var suggestionRequest = new SuggestionRequest(partyRequested, pricingCategory);
        var foundedSuggestions = new List<SuggestionMade>();

        for (var i = 0; i < NumberOfSuggestionsPerPricingCategory; i++)
        {
            var seatOptionsSuggested = auditoriumSeating.SuggestSeatingOptionFor(suggestionRequest);

            if (seatOptionsSuggested.MatchExpectation())
            {
                // We get the new version of the Auditorium after the allocation
                auditoriumSeating = auditoriumSeating.Allocate(seatOptionsSuggested);

                foundedSuggestions.Add(new SuggestionMade(seatOptionsSuggested));
            }
        }

        return foundedSuggestions;
    }
}