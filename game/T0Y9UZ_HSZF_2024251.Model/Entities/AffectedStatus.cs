using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace T0Y9UZ_HSZF_2024251.Model.Entities
{
    public class AffectedStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("health")]
        public int Health { get; set; }

        [JsonPropertyName("hunger")]
        public int Hunger { get; set; }

        [JsonPropertyName("thirst")]
        public int Thirst { get; set; }

        [JsonPropertyName("fatigue")]
        public int Fatigue { get; set; }
    }
}
