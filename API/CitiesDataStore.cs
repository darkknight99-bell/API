using API.Models;

namespace API
{
    public class CitiesDataStore
    {
        public List<CityDTO> Cities { get; set; }
        public static CitiesDataStore Current { get; set; } = new CitiesDataStore(); //current means this current instance

        public CitiesDataStore()
        {
            Cities = new List<CityDTO>()
            {
                new CityDTO()
                {
                    Id = 1,
                    Name = "Lagos",
                    Description = "City that never sleeps",
                    PointOfInterest = new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id= 1,
                            Name = "Badagry",
                            Description = "Slave Trade Center"
                        },
                         new PointOfInterestDTO()
                        {
                            Id= 2,
                            Name = "Lagos Island",
                            Description = "Large water bodies"
                        },
                    }
                },
                new CityDTO()
                {
                    Id = 2,
                    Name = "New York",
                    Description = "Big apple",
                    PointOfInterest = new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id= 1,
                            Name = "Central Park",
                            Description = "recreational Park"
                        },
                         new PointOfInterestDTO()
                        {
                            Id= 2,
                            Name = "Lady Liberty",
                            Description = "Statue of freedom"
                        },
                    }
                }
            };
        }
    }
}



