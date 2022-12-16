using System;
using System.Collections.Generic;
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
        
        public double Housing { get; set; }
        
        public double CostofLiving { get; set; }
        
        public double Startups { get; set; }
        
        public double TravelConnectivity { get; set; }
        
        public double Commute { get; set; }
        
        public double BusinessFreedom { get; set; }
        
        public double Safety { get; set; }
        
        public double Healthcare { get; set; }
        
        public double Education { get; set; }
        
        public double EnvironmentalQuality { get; set; }
        
        public double Economy { get; set; }
        
        public double Taxation { get; set; }
        
        public double InternetAccess { get; set; }
        
        public double LeisureCulture { get; set; }
        
        public double Tolerance { get; set; }
        
        public double Outdoors { get; set; }

        public UaScoresDataFrame()
        {

        }

        public UaScoresDataFrame(int cityId, string cityName, string country, string continent, double housing, double costofLiving, double startups, double travelConnectivity, double commute, double businessFreedom, double safety, double healthcare, double education, double environmentalQuality, double economy, double taxation, double internetAccess, double leisureCulture, double tolerance, double outdoors)
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

        
    }
}
