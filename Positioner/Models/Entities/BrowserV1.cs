using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Positioner.Models
{
    internal class BrowserSpecV1
    {
        [JsonPropertyName("position")]
        public Position Position { get; set; }

        [JsonPropertyName("urls")]
        public IReadOnlyList<string> Urls { get; set; }
    }
}
