using Microsoft.ML.Data;

namespace ISIS_PROJEKAT.DTOs
{
    public class LoadPrediction
    {
        [ColumnName("Score")]
        public float Load { get; set; }

        public DateTime DateTime { get; set; }
    }
}
