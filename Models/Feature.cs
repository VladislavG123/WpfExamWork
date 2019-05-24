using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthquakeApp.Models
{
    public class Feature
    {
        [JsonProperty("properties")]
        public Information Information { get; set; }
        public string Id { get; set; }
    }
}
