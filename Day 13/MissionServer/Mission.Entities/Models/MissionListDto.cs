using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Entities.Models
{
    public class MissionListDto
    {
        
          public int Id { get; set; }
          public string MissionTitle { get; set; }
          public string MissionDescription { get; set; }
        public string MissionThemeName { get; set; }
        public DateTime StartDate { get; set; }
          public DateTime EndDate { get; set; }
        

    }
}
