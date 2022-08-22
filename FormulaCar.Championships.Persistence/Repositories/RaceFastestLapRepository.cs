using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories
{
    public class RaceFastestLapRepository:RepositoryBase<RaceFastesLap>,IRaceFastesLapRepository
    {
        public RaceFastestLapRepository(RepositoryDbContext repositoryContext) :base(repositoryContext)
        {
            
        }
    }
}
