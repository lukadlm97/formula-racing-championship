using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class MediaTagRepository : RepositoryBase<MediaTag>, IMediaTagRepository
{
    public MediaTagRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }
}