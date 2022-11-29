using System.ComponentModel.DataAnnotations;

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
    }
}
