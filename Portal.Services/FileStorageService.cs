using Portal.Data.Entities;
using Portal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class FileStorageService : IBaseService<FileStorage>
    {
        private readonly IBaseRepository<FileStorage> _baseRepository;
        public FileStorageService(IBaseRepository<FileStorage> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public int DeleteWithIds(Guid[] id)
        {
            return _baseRepository.DeleteWithIds(id);
        }

        public IEnumerable<FileStorage> GetAll()
        {
            return _baseRepository.GetAll();
        }

        public FileStorage GetWithId(Guid id)
        {
            return _baseRepository.GetWithId(id);
        }

        public FileStorage Save(FileStorage data)
        {
            return _baseRepository.Save(data);
        }

        public FileStorage Update(FileStorage data)
        {
            return _baseRepository.Update(data);
        }
    }
}
