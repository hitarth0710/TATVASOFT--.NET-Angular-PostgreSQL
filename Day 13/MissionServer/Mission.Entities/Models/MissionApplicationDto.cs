using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Entities.Models
{
    public class MissionApplicationDto
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public int UserId { get; set; }
        public DateTime AppliedDate { get; set; }
        public bool Status { get; set; }
        public int Seats { get; set; }
        public string MissionTitle { get; set; }
        public string MissionTheme { get; set; }
        public string FirstName { get; set; }

    }
}
