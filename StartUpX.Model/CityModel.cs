﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class CityModel
    {
        public int CityId { get; set; }

        public string? CityName { get; set; }

        public int StateId { get; set; }

        public bool IsActive { get; set; }
    }
}
