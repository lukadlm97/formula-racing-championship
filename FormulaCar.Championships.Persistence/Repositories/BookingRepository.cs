using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
{
    public BookingRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }
}