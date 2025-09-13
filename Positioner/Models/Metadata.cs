﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Positioner.Models
{
    public class Metadata
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("created-timestamp")]
        public DateTime CreatedTimestamp { get; set; }

        [JsonPropertyName("updated-timestamp")]
        public DateTime UpdatedTimestamp { get; set; }
    }
}
