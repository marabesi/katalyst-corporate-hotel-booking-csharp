﻿namespace CorporateHotelBooking.Hotels.Domain;

public record HotelId(Guid Value)
{
    public static HotelId New()
    {
        return From(Guid.NewGuid());
    }

    public static HotelId From(Guid value)
    {
        return new HotelId(value);
    }

}