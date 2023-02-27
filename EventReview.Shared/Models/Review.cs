using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReview.Shared.Models
{
    public class Review
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string AuthorName { get; set; }

        public string Comment { get; set; }

        [Range(1, 10, ErrorMessage = "Please provide a value between 1 and 5.")]
        public int ReviewPointsFrom0To10 { get; set; }
    }
}
