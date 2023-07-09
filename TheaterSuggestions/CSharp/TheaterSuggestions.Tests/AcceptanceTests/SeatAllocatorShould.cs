using ExternalDependencies.AuditoriumLayoutRepository;
using ExternalDependencies.ReservationsProvider;
using NFluent;
using NUnit.Framework;

namespace SeatsSuggestions.Tests.AcceptanceTests;

[TestFixture]
public class SeatAllocatorShould
{
    [Test]
    public void Return_SeatsNotAvailable_when_Auditorium_has_all_its_seats_already_reserved()
    {
        // Madison Auditorium-5
        //
        //      1   2   3   4   5   6   7   8   9  10
        // A : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        // B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        const string showId = "5";
        const int partyRequested = 1;

        var auditoriumLayoutAdapter =
            new AuditoriumSeatingAdapter(new AuditoriumLayoutRepository(), new ReservationsProvider());

        var seatAllocator = new SeatAllocator(auditoriumLayoutAdapter);

        var suggestionsMade = seatAllocator.MakeSuggestions(showId, partyRequested);
        Check.That(suggestionsMade.PartyRequested).IsEqualTo(partyRequested);
        Check.That(suggestionsMade.ShowId).IsEqualTo(showId);

        Check.That(suggestionsMade).IsInstanceOf<SuggestionNotAvailable>();
    }

    [Test]
    public void Suggest_one_seat_when_Auditorium_contains_one_available_seat_only()
    {
        // Ford Auditorium-1
        //
        //       1   2   3   4   5   6   7   8   9  10
        //  A : (2) (2)  1  (1) (1) (1) (1) (1) (2) (2)
        //  B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        const string showId = "1";
        const int partyRequested = 1;

        var auditoriumLayoutAdapter =
            new AuditoriumSeatingAdapter(new AuditoriumLayoutRepository(), new ReservationsProvider());

        var seatAllocator = new SeatAllocator(auditoriumLayoutAdapter);

        var suggestionsMade = seatAllocator.MakeSuggestions(showId, partyRequested);

        Check.That(suggestionsMade.SeatNames(PricingCategory.First)).ContainsExactly("A3");
    }
}