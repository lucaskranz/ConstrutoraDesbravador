using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstrutoraDesbravador.Business.Models
{
    [NotMapped]
    public class RandomUserResponse
    {
        [JsonProperty("results")]
        public List<User> Results { get; set; }
    }
    public class User
    {
        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class Name
    {
        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
    }
}
