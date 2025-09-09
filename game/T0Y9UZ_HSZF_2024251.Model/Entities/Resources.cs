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
    public class Resources
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("food")]
        public int Food { get; set; }

        [JsonPropertyName("water")]
        public int Water { get; set; }

        [JsonPropertyName("weapons")]
        public int Weapons { get; set; }

        [JsonPropertyName("alchemy_ingredients")]
        public int AlchemyIngredients { get; set; }
    }
}
