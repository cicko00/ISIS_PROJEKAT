using System.ComponentModel.DataAnnotations;

namespace ISIS_PROJEKAT.Models
{

    public class LoadDataHistory
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public string? City { get; set; }

        
        public string? District { get; set; }

        
        public string? TimeZone { get; set; }

        public string? PTID { get; set; }

        
        public double? Load { get; set; }

        public bool isWeekend { get; set; } = false;

        
        public double? Temperature { get; set; }

        
        public double? FeelsLike { get; set; }

        
        public double? Dew { get; set; }

        
        public double? Humidity { get; set; }

        
        public double? Precip { get; set; }

       
        public double? Snow { get; set; }

        
        public double? SnowDepth { get; set; }

        
        public double? WindGust { get; set; }

        
        public double? WindSpeed { get; set; }

        
        public double? WindDir { get; set; }

        
        public double? SeaLevelPressure { get; set; }

        
        public double? CloudCover { get; set; }

        
        public double? Visibilty { get; set; }

        
        public double? UVIndex { get; set; }

        
        public string? Conditions { get; set; }


    }
}
