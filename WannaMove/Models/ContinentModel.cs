namespace WannaMove.Models
{
    public class ContinentModel
    {
        public ContinentModel(string continent, bool isActive)
        {
            this.continent = continent;
            this.IsActive = isActive;
        }

        public string continent { get; set; }    
        public bool IsActive { get; set; }  
    }


}
