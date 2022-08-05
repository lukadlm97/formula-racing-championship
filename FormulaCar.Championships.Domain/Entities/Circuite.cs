using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Entities
{
    public class Circuite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Length { get; set; }
        public int Capacity { get; set; }
        //public int MediaId { get; set; }
       // public MediaTag MediaTag { get; set; }
    }
}
