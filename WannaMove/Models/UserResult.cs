using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WannaMove.Models
{
    public class UserResult
    {
        [Key]
        public int ResultId { get; set; }
        public string Criteria { get; set; }
        public string FilteredData { get; set; }
        public string Result { get; set; }
        public UserResult UserResults { get; set; }

        public int UserID { get; set; } 
    }
}
