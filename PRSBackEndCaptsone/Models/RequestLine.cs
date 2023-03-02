using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PRSBackEndCaptsone.Models
{
    public class RequestLine
    {

        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int RequestId { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }


        public int Quantity { get; set; }


        //[ForeignKey(nameof(RequestId))]

        //relationship
        [JsonIgnore]
        [ForeignKey(nameof(RequestId))]
        public Request Request { get; set; }

        //relationship
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
