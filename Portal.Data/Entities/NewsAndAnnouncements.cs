using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data.Entities
{
    public class NewsAndAnnouncements : IEntity
    {
        [Key]
        [Required]
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string? VideoImage { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int Order { get; set; }
        public Guid? FileId { get; set; }
        public FileStorage FileStorage { get; set; }
    }
}
