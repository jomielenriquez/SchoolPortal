using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data.Entities
{
    public class FileStorage : IEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public int FileType { get; set; }
        [Required]
        public string FileDownloadName { get; set; }
    }
}
