﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class RaceBestSectorTimesDto
    {
        public string Driver { get; set; }
        public string Constructor { get; set; }
        public string Circuite { get; set; }
        public string Position { get; set; }
        public TimeSpan Time { get; set; }
        public int Sector { get; set; }
    }
}