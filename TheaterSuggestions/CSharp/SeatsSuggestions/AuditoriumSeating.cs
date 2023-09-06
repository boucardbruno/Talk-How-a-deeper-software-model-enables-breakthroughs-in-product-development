using System.Collections.Generic;
using Value;
using Value.Shared;

namespace SeatsSuggestions;

public class AuditoriumSeating : ValueType<AuditoriumSeating>
{
    private Dictionary<string, Row> _rows;

    public AuditoriumSeating(Dictionary<string, Row> rows)
    {
        _rows = rows;
    }

    public IReadOnlyDictionary<string, Row> Rows => _rows;

    public SeatingOptionSuggested SuggestSeatingOptionFor(SuggestionRequest suggestionRequest)
    {
        foreach (var row in _rows.Values)
        {
            var seatingOption = row.SuggestSeatingOption(suggestionRequest);

            if (seatingOption.MatchExpectation()) return seatingOption;
        }

        return new SeatingOptionNotAvailable(suggestionRequest);
    }

    public AuditoriumSeating Allocate(SeatingOptionSuggested seatingOptionSuggested)
    {
        // Update the seat references in the Auditorium
        var newVersionOfRows = new Dictionary<string, Row>(_rows);

        foreach (var updatedSeat in (IEnumerable<SeatingPlace>)seatingOptionSuggested.Seats)
        {
            var formerRow = newVersionOfRows[updatedSeat.RowName];
            var newVersionOfRow = formerRow.Allocate(updatedSeat);
            newVersionOfRows[updatedSeat.RowName] = newVersionOfRow;
        }

        _rows = newVersionOfRows;

        return this;
    }

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return new object[] { new DictionaryByValue<string, Row>(_rows) };
    }

    private AuditoriumSeating AllocateSeats(IEnumerable<SeatingPlace> updatedSeats)
    {
        var newVersionOfRows = new Dictionary<string, Row>(_rows);

        foreach (var updatedSeat in updatedSeats)
        {
            var formerRow = newVersionOfRows[updatedSeat.RowName];
            var newVersionOfRow = formerRow.Allocate(updatedSeat);
            newVersionOfRows[updatedSeat.RowName] = newVersionOfRow;
        }

        _rows = newVersionOfRows;

        return this;
    }
}