using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using T0Y9UZ_HSZF_2024251.Model.Types;

namespace T0Y9UZ_HSZF_2024251.Model.Entities
{
    public class Hero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public HeroStatus Status { get; set; }

        [JsonPropertyName("health_status")]
        public HealthStatus HealthStatus { get; set; }

        [JsonPropertyName("health")]
        public int Health { get; set; }

        [JsonPropertyName("hunger")]
        public int Hunger { get; set; }

        [JsonPropertyName("thirst")]
        public int Thirst { get; set; }

        [JsonPropertyName("fatigue")]
        public int Fatigue { get; set; }

        [JsonPropertyName("abilities")]
        public List<string> Abilities { get; set; }

        [JsonPropertyName("resources")]
        public Resources Resources { get; set; }

        [JsonPropertyName("daysleft")]
        public int DaysLeft { get; set; }

        [JsonPropertyName("tasks")]
        public List<GameTask> Tasks { get; set; }

        [JsonPropertyName("defeatedmonsters")]
        public List<string> DefeatedMonsters { get; set; }

        [JsonPropertyName("completedtasks")]
        public List<string> CompletedTasks { get; set; }
    }
}
