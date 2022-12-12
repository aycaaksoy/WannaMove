using System;
using System.ComponentModel.DataAnnotations;
using WannaMove.Data;

namespace WannaMove.Models
{
    public class UaScoresDataFrame
    {
        [Key]
        public int CityId { get; set; }
        
        public string CityName { get; set; }
        
        public string Country { get; set; }
        public string Continent { get; set; }
        
        public decimal Housing { get; set; }
        
        public decimal CostofLiving { get; set; }
        
        public decimal Startups { get; set; }
        
        public decimal TravelConnectivity { get; set; }
        
        public decimal Commute { get; set; }
        
        public decimal BusinessFreedom { get; set; }
        
        public decimal Safety { get; set; }
        
        public decimal Healthcare { get; set; }
        
        public decimal Education { get; set; }
        
        public decimal EnvironmentalQuality { get; set; }
        
        public decimal Economy { get; set; }
        
        public decimal Taxation { get; set; }
        
        public decimal InternetAccess { get; set; }
        
        public decimal LeisureCulture { get; set; }
        
        public decimal Tolerance { get; set; }
        
        public decimal Outdoors { get; set; }

        public UaScoresDataFrame()
        {

        }

        public UaScoresDataFrame(int cityId, string cityName, string country, string continent, decimal housing, decimal costofLiving, decimal startups, decimal travelConnectivity, decimal commute, decimal businessFreedom, decimal safety, decimal healthcare, decimal education, decimal environmentalQuality, decimal economy, decimal taxation, decimal internetAccess, decimal leisureCulture, decimal tolerance, decimal outdoors)
        {
            CityId = cityId;
            CityName = cityName;
            Country = country;
            Continent = continent;
            Housing = housing;
            CostofLiving = costofLiving;
            Startups = startups;
            TravelConnectivity = travelConnectivity;
            Commute = commute;
            BusinessFreedom = businessFreedom;
            Safety = safety;
            Healthcare = healthcare;
            Education = education;
            EnvironmentalQuality = environmentalQuality;
            Economy = economy;
            Taxation = taxation;
            InternetAccess = internetAccess;
            LeisureCulture = leisureCulture;
            Tolerance = tolerance;
            Outdoors = outdoors;
        }

        internal static void Insert(UaScoresDataFrame uaScoresDataFrame)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Add(uaScoresDataFrame);
                context.SaveChanges();

            };
        }
    }
}
