using CorporateHotelBooking.Test.Unit;
using FluentAssertions;

namespace CorporateHotelBooking.Test.E2E
{
    public class CorporateHotelAcceptanceTest
    {
        private readonly HotelService _hotelService;
        private readonly BookingService _bookingService;
        private readonly EmployeeService _employeeService;
        private HotelRepository _hotelRepository;

        public CorporateHotelAcceptanceTest()
        {
            _employeeService = new EmployeeService();
            _bookingService = new BookingService();
            _hotelRepository = new InMemoryHotelRepository();
            _hotelService = new HotelService(_hotelRepository);
        }

        [Fact]
        public async Task an_employee_should_be_able_to_book_a_room()
        {
            // Arrange
            var companyId = 1;
            var hotelId = 1;
            var hotelName = "Wesing";
            var employeeId = 1;
            var roomType = "Suite";
            var roomNumber = 1;
            var checkIn = DateTime.Today;
            var checkOut = DateTime.Today.AddDays(7);
            _hotelService.AddHotel(hotelId, hotelName);
            _hotelService.SetRoom(hotelId, roomNumber, roomType);
            _employeeService.AddEmployee(companyId, employeeId);

            // Act
            var booking = _bookingService.Book(employeeId, hotelId, roomType, checkIn, checkOut);

            // Assert
            booking.HotelId.Should().Be(hotelId);
            booking.BookedBy.Should().Be(employeeId);
            booking.RoomType.Should().Be(roomType);
            booking.RoomNumber.Should().Be(roomNumber);
            booking.CheckIn.Should().BeSameDateAs(checkIn);
            booking.CheckOut.Should().BeSameDateAs(checkOut);
        }
    }
}