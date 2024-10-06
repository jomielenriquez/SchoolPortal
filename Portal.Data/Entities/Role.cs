using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data.Entities
{
    public class Role : IEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }

        public virtual ICollection<Admin> Admins { get; set; }
    }
}
