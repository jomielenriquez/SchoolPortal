using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data.Entities
{
    public class Admin
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string FIRSTNAME { get; set; }
        [Required]
        public string LASTNAME { get; set; }
        [Required]
        public string USERNAME { get; set; }
        [Required]
        public string PASSWORD { get; set; }
        [Required]
        public int ROLEID { get; set; }
        public Role Role { get; set; }
    }
}
