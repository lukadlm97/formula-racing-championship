using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAll();
        Task<BookingDto> Create(BookingForCreationDto bookingForCreationDto);
        Task<bool> Exist(string driver, string constructor, string season);
        Task<IEnumerable<BookingDto>> GetBookingsBySeason( string season);
    }
}
