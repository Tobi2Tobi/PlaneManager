using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Models
{
    public class PlaneCreateDTO
    {
        public string Name { get; set; }
        public int Seats { get; set; }
        public bool IsActive { get; set; }
    }
}
