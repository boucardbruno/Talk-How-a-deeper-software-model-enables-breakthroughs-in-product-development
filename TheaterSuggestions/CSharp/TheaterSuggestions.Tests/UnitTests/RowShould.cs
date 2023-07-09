using System.Collections.Generic;
using System.Linq;
using ExternalDependencies.AuditoriumLayoutRepository;
using ExternalDependencies.ReservationsProvider;
using NFluent;
using NUnit.Framework;

namespace SeatsSuggestions.Tests.UnitTests;

[TestFixture]
public class RowShould
{
    [Test]
    public void Be_a_Value_Type()
    {
        var a1 = new Seat("A", 1, PricingCategory.Second, SeatAvailability.Available);
        var a2 = new Seat("A", 2, PricingCategory.Second, SeatAvailability.Available);

        // Two different instances with same values should be equals
        var rowFirstInstance = new Row("A", new List<Seat> { a1, a2 });
        var rowSecondInstance = new Row("A", new List<Seat> { a1, a2 });
        Check.That(rowSecondInstance).IsEqualTo(rowFirstInstance);

        // Should not mutate existing instance 
        var a3 = new Seat("A", 3, PricingCategory.Second, SeatAvailability.Available);
        var newRowWithNewSeatAdded = rowSecondInstance.AddSeat(a3);

        Check.That(rowSecondInstance).IsEqualTo(rowFirstInstance);
        Check.That(newRowWithNewSeatAdded).IsNotEqualTo(rowFirstInstance);
    }

    [Test]
    public void Offer_several_suggestions_ie_1_per_PricingCategory_and_other_one_without_category_affinity()
    {
        // New Amsterdam-18
        //
        //     1   2   3   4   5   6   7   8   9  10
        //  A: 2   2   1   1   1   1   1   1   2   2
        //  B: 2   2   1   1   1   1   1   1   2   2
        //  C: 2   2   2   2   2   2   2   2   2   2
        //  D: 2   2   2   2   2   2   2   2   2   2
        //  E: 3   3   3   3   3   3   3   3   3   3
        //  F: 3   3   3   3   3   3   3   3   3   3
        const string eventId = "18";
        const int partyRequested = 1;

        var auditoriumLayoutAdapter =
            new AuditoriumSeatingAdapter(new AuditoriumLayoutRepository(), new ReservationsProvider());

        var seatAllocator = new SeatAllocator(auditoriumLayoutAdapter);

        var suggestionsMade = seatAllocator.MakeSuggestions(eventId, partyRequested);

        Check.That(suggestionsMade.SeatNames(PricingCategory.First)).ContainsExactly("A3", "A4", "A5");
        Check.That(suggestionsMade.SeatNames(PricingCategory.Second)).ContainsExactly("A1", "A2", "A9");
        Check.That(suggestionsMade.SeatNames(PricingCategory.Third)).ContainsExactly("E1", "E2", "E3");
        Check.That(suggestionsMade.SeatNames(PricingCategory.Mixed)).ContainsExactly("A1", "A2", "A3");
    }

}
