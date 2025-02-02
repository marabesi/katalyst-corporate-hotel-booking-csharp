﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateHotelBooking.Hotels.Application;
using CorporateHotelBooking.Hotels.Domain;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Test.Unit.UseCases
{
    public class AddHotelUseCaseTest
    {
        private readonly Mock<IHotelRepository> _hotelRepository;

        public AddHotelUseCaseTest()
        {
            _hotelRepository = new();
        }

        [Fact]
        public void should_store_hotel()
        {
            // Arrange
            Guid hotelId = Guid.NewGuid();
            string hotelName = "Westing";
            var addHotelUseCase = new AddHotelUseCase(_hotelRepository.Object);

            // Act
            addHotelUseCase.Execute(hotelId, hotelName);

            // Assert
            _hotelRepository.Verify(x => x.Add(It.IsAny<Hotel>()), Times.Once());
        }

        [Fact]
        public void should_throw_exception_when_hotel_already_exists()
        {
            // Arrange
            Guid hotelId = Guid.NewGuid();
            string hotelName = "Westing";
            var addHotelUseCase = new AddHotelUseCase(_hotelRepository.Object);
            _hotelRepository.Setup(x => x.Get(HotelId.From(hotelId))).Returns(new Hotel(HotelId.From(hotelId), "Name"));

            // Act
            Action action = () => addHotelUseCase.Execute(hotelId, hotelName);

            // Assert
            action.Should().Throw<ExistingHotelException>();

        }
    }
}
