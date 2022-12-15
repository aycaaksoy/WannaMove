using System.Collections.Generic;

namespace WannaMove.Models
{
    public class ContinentListViewModel
    {
        public List<UaScoresDataFrame> UaScoresDataFrames { get; set; }
        public List<string> Continents { get; set; }
        public List<string> SelectedContinents { get; set; }
        public List<string> Countries { get; set; }
        public List<string> SelectedCountries { get; set; }


    }
}
