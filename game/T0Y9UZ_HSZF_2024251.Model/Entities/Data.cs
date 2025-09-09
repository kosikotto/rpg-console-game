using System.Text.Json.Serialization;

namespace T0Y9UZ_HSZF_2024251.Model.Entities;

public class Data
{
    [JsonPropertyName("heroes")]
    public List<Hero> Heroes { get; set; }

    [JsonPropertyName("tasks")]
    public List<GameTask> Tasks { get; set; }

    [JsonPropertyName("monsters")]
    public List<Monster> Monsters { get; set; }

    [JsonPropertyName("resources")]
    public List<Resources> Resources { get; set; }

    [JsonPropertyName("days")]
    public int Days { get; set; }
}
