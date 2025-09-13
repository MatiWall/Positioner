
using System.Text.Json.Serialization;

namespace Positioner.Models
{
    public interface IEntity
    {   
        string Version { get; }
        string Kind { get; }
        Metadata Metadata { get; }
        object Spec { get; }
    }

    internal class Entity<TSpec> : IEntity
    {
        [JsonPropertyName("version")]
        public string Version {  get; set; }

        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }

        [JsonPropertyName("spec")]
        public TSpec Spec {  get; set; }

        object IEntity.Spec => Spec;

    }
}
