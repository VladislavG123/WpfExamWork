using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthquakeApp.Models
{
    public class ShortInfo
    {
        public double? Magnitude { get; set; }
        public string Place { get; set; }
        public DateTime Time { get; set; }
        public double? Depth { get; set; }
    }
}
