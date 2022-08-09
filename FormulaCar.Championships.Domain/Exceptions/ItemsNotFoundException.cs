using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Exceptions
{
    public class ItemsNotFoundException : NotFoundException
    {
        public ItemsNotFoundException(string collectionName) : base($"Items for collection {collectionName} was not found")
        {
        }
    }
}
