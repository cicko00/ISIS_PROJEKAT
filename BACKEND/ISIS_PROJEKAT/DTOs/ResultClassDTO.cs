namespace ISIS_PROJEKAT.DTOs
{
    public class ResultClassDTO
    {
        public DateTime DateTime { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public float Load { get; set; }

        public bool isWeekend { get; set; }
    }
}
