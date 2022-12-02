namespace WannaMove.Models
{
    public class ContinentModel
    {
        public ContinentModel(string Continent, bool isActive)
        {
            this.Continent = Continent;
            this.IsActive = isActive;
        }

        public string Continent { get; set; }    
        public bool IsActive { get; set; }  
    }


}
