using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Entities.Entities
{
    public class MissionSkill : BaseEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("skill_name")]
        public string SkillName { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }
}
