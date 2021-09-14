﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaWebUI.Library.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public decimal NormalPrice { get; set; }
        public decimal StudentPrice { get; set; }
        public int Capacity { get; set; }
    }
}
