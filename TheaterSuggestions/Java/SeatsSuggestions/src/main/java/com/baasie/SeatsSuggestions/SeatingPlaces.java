package com.baasie.SeatsSuggestions;

import lombok.Getter;

public class SeatingPlaces {

    private String rowName;
    private int number;
    @Getter
    private PricingCategory pricingCategory;
    private SeatAvailability seatAvailability;

    public SeatingPlaces(String rowName, int number, PricingCategory pricingCategory, SeatAvailability seatAvailability) {
        this.rowName = rowName;
        this.number = number;
        this.pricingCategory = pricingCategory;
        this.seatAvailability = seatAvailability;
    }

    public boolean isAvailable() {
        return seatAvailability == SeatAvailability.Available;
    }


    public void allocate() {
        if (seatAvailability == SeatAvailability.Available) {
            seatAvailability = SeatAvailability.Allocated;
        }
    }

    @Override
    public String toString() {
        return rowName + number;
    }
}
