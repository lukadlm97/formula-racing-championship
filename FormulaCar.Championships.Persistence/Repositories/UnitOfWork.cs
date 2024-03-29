﻿using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly RepositoryDbContext _dbContext;

    public UnitOfWork(RepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}