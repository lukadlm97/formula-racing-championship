﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class QualificationSpeedTrapDto
    {
        public string Driver { get; set; }
        public string Constructor { get; set; }
        public string Circuite { get; set; }
        public string Position { get; set; }
        public int Season { get; set; }
        public double MaxSpeed { get; set; }
    }
}