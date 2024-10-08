using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace BracketGenerator.Model
{
    public class Team
    {
        public string Seed { get; set; }
        
        [JsonPropertyName("Team")]
        public string Country { get; set; }
        public string Group { get; set; }
    }
}
