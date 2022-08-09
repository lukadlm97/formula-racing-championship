﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction
{
    public interface IQualificationPeriodsService
    {
        Task<IEnumerable<QualificationPeriodsDto>> GetAll(CancellationToken cancellationToken = default);
    }
}
