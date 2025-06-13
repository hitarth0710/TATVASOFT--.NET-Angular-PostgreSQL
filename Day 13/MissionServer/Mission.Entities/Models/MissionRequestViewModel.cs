using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mission.Entities.Models
{
    public class MissionRequestViewModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        [Required]
        public string MissionTitle { get; set; }
        public int MissionThemeId { get; set; }
        [Required]
        public string MissionDescription { get; set; }

        [JsonPropertyName("totalSheets")]
        public int TotalSeats { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string MissionImages { get; set; }
        public string MissionSkillId { get; set; }
    }
}
