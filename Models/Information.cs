using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthquakeApp.Models
{
    [JsonObject("Properties")]
    public class Information
    {
        [JsonProperty("mag")]
        public double? Magnitude { get; set; }
        public string Place { get; set; }
        public double? Time { get; set; }

        private const double KILOMETERS_IN_ONE_DEGREE = 111.2; 
        private double? _depth;
        [JsonProperty("dmin")]
        public double? Depth { get { return _depth * KILOMETERS_IN_ONE_DEGREE; } set { _depth = value; } }
    }
}
