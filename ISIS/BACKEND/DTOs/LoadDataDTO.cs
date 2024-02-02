namespace ISIS_PROJEKAT.DTOs
{
    public class LoadDataDTO
    {
        public DateTime DateTime { get; set; }

        public float Load { get; set; }

        public bool isWeekend { get; set; } = false;


        public float Temperature { get; set; }


        public float FeelsLike { get; set; }


        public float Dew { get; set; }


        public float Humidity { get; set; }


        public float  Precip { get; set; }


        public float Snow { get; set; }


        public float SnowDepth { get; set; }


        //public double? WindGust { get; set; }


        //public double? WindSpeed { get; set; }


        //public double? WindDir { get; set; }

        //public double? CloudCover { get; set; }

        //public double? Visibilty { get; set; }


       


       
    }
}
