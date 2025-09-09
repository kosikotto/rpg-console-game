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
    public class GameTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("required_resources")]
        public Resources RequiredResources { get; set; }

        [JsonPropertyName("affected_status")]
        public AffectedStatus AffectedStatus { get; set; }

        public GameTask()
        {

        }
    }
}
