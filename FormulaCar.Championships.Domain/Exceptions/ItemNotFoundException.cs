using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Exceptions
{
    public class ItemNotFoundException:NotFoundException
    {
        public ItemNotFoundException(int id) : base($"Item with identifier {id} was not found" )
        {
        }
    }
}
