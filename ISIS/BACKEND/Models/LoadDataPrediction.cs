using System.ComponentModel.DataAnnotations;

namespace ISIS_PROJEKAT.Models
{
    public class LoadDataPrediction
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }

        public string? City { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public float Load { get; set; }
    }
}
