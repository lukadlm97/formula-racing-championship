﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class QualificationSpeedTrapForCreationDto
    {
        public string Driver { get; set; }
        public string Constructor { get; set; }
        public string Circuite { get; set; }
        public string Position { get; set; }
        public string Season { get; set; }
        public double MaxSpeed { get; set; }
    }
}