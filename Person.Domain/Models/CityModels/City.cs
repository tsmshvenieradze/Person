using Person.Shared.Utils;

namespace Person.Domain.Models.CityModels
{
    public class City : Entity
    {
        public string CityName { get; set; }

        public static City Create(string cityName)
        {
            return new City
            {
                CreateDate =  DateTimeUtils.Now,
                CityName = cityName
            };
        }
    }
}
