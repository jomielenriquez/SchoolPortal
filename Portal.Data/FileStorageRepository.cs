using Portal.Data.Data;
using Portal.Data.Entities;
using Portal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data
{
    public class FileStorageRepository : IBaseRepository<FileStorage>
    {
        private readonly PortalDBContext _dbContext;
        public FileStorageRepository(PortalDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int DeleteWithIds(Guid[] id)
        {
            var filesToDelete = _dbContext.FileStorage.Where(f => id.Contains(f.Id)).ToList();

            if (filesToDelete.Any())
            {
                _dbContext.FileStorage.RemoveRange(filesToDelete);
                return _dbContext.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<FileStorage> GetAll()
        {
            return _dbContext.FileStorage.ToList();
        }

        public FileStorage GetWithId(Guid id)
        {
            return _dbContext.FileStorage.FirstOrDefault(f => f.Id == id);
        }

        public FileStorage Save(FileStorage data)
        {
            _dbContext.FileStorage.Add(data);
            _dbContext.SaveChanges();
            return data;
        }

        public FileStorage Update(FileStorage data)
        {
            var existingFile = _dbContext.FileStorage.FirstOrDefault(f => f.Id == data.Id);

            if (existingFile != null)
            {
                // Update fields in the existing record
                existingFile.FileName = data.FileName;
                existingFile.FileType = data.FileType;
                existingFile.FileDownloadName = data.FileDownloadName;

                // Save changes to the database
                _dbContext.SaveChanges();
                return existingFile; // Return the updated record
            }

            return null;
        }
    }
}
