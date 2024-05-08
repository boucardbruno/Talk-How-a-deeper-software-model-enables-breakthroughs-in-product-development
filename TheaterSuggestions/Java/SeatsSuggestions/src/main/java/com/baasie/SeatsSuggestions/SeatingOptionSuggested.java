package com.baasie.SeatsSuggestions;

import java.util.ArrayList;
import java.util.List;

public class SeatingOptionSuggested {

    private PricingCategory pricingCategory;
    private List<SeatingPlaces> seats = new ArrayList<>();
    private int partyRequested;

    public SeatingOptionSuggested(int partyRequested, PricingCategory pricingCategory) {
        this.pricingCategory = pricingCategory;
        this.partyRequested = partyRequested;
    }

    public void addSeat(SeatingPlaces seat) {
        seats.add(seat);
    }

    public boolean matchExpectation() {
        return seats.size() == partyRequested;
    }

    public List<SeatingPlaces> seats() {
        return seats;
    }
}
