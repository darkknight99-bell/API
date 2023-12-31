﻿namespace API.Models
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPointOfInterest
        {
            get
            {
                return PointOfInterest.Count;
            }
        }
        public ICollection<PointOfInterestDTO> PointOfInterest { get; set;} 
            = new List<PointOfInterestDTO>(); //always initialize collection to avoid null reference issues

    }
}
