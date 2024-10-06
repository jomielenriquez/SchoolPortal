using Portal.Data.Entities;
using Portal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class FileStorageService : BaseService<FileStorage>
    {
        private readonly IBaseRepository<FileStorage> _baseRepository;
        public FileStorageService(IBaseRepository<FileStorage> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
    }
}
