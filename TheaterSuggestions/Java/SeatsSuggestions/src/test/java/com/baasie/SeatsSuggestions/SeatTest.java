package com.baasie.SeatsSuggestions;

import org.junit.Test;

import static com.google.common.truth.Truth.assertThat;

public class SeatTest {


    @Test
    public void Be_a_Value_Type() {
        SeatingPlaces firstInstance = new SeatingPlaces("A", 1, PricingCategory.Second, SeatAvailability.Available);
        SeatingPlaces secondInstance = new SeatingPlaces("A", 1, PricingCategory.Second, SeatAvailability.Available);

        // Two different instances with same values should be equals
        assertThat(secondInstance).isEqualTo(firstInstance);

        // Should not mutate existing instance
        secondInstance.allocate();
        assertThat(secondInstance).isEqualTo(firstInstance);
    }
}