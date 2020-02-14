﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadaAcademy.DataModels
{
    public class ItemRoute
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Order { get; set; }
        public DateTime ActivityTime { get; set; }

    }
}