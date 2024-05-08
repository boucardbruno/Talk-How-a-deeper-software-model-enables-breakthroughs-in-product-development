package com.baasie.SeatsSuggestions;

import org.junit.Test;

import java.util.Arrays;

import static com.google.common.truth.Truth.assertThat;

public class RowTest {

    @Test
    public void be_a_Value_Type() {
        SeatingPlaces a1 = new SeatingPlaces("A", 1, PricingCategory.Second, SeatAvailability.Available);
        SeatingPlaces a2 = new SeatingPlaces("A", 2, PricingCategory.Second, SeatAvailability.Available);

        // Two different instances with same values should be equals
        Row rowFirstInstance = new Row("A", Arrays.asList(a1, a2));
        Row rowSecondInstance = new Row("A", Arrays.asList(a1, a2));
        assertThat(rowSecondInstance).isEqualTo(rowFirstInstance);
    }

}