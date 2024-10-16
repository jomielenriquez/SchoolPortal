﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data.Entities
{
    public class SystemParameterType : IEntity
    {
        [Key]
        [Required]
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<SystemParameter> SystemParameters { get; set;}
    }
}
