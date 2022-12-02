namespace WannaMove.Models
{
    public class UserDataViewModel
    {
        public UserDataViewModel(int cityId, string cityName, string country, string continent)
        {
            CityId = cityId;
            CityName = cityName;
            Country = country;
            Continent = continent;
        }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public string Country { get; set; }
        public string Continent { get; set; }
    }
}
