using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories
{
    public class QualificationClassificationRepository:RepositoryBase<QualificationClassification>,IQualificationClassificationRepository
    {
        public QualificationClassificationRepository(RepositoryDbContext repositoryDbContext):base(repositoryDbContext)
        {
            
        }
    }
}
