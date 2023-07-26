using System;
namespace UserApi.DTO
{
    public class WeightLogCreationDTO
    {
        public DateTime Date { get; set; }
        public double Weight { get; set; }
        public int UserId { get; set; }
    }
}

