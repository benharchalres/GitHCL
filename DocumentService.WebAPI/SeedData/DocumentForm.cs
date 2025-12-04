using Newtonsoft.Json;

namespace DocumentService.WebAPI.SeedData
{
    public class DocumentForm
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
