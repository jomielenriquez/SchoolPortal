using Microsoft.EntityFrameworkCore;
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
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly PortalDBContext _portalDBContext;
        public BaseRepository(PortalDBContext portalDBContext)
        {
            _portalDBContext = portalDBContext;
        }
        public int DeleteWithIds(Guid[] id)
        {
            var entitiesToDelete = _portalDBContext.Set<T>().Where(e => id.Contains(((IEntity)e).Id)).ToList();

            if (entitiesToDelete.Any())
            {
                _portalDBContext.Set<T>().RemoveRange(entitiesToDelete);
                return _portalDBContext.SaveChanges();
            }

            return 0;
        }

        public IEnumerable<T> GetAll()
        {
            return _portalDBContext.Set<T>().ToList();
        }

        public T GetWithId(Guid id)
        {
            return _portalDBContext.Set<T>().Find(id);
        }

        public T Save(T data)
        {
            _portalDBContext.Set<T>().Add(data);
            _portalDBContext.SaveChanges();
            return data;
        }

        public T Update(T data)
        {
            _portalDBContext.Entry(data).State = EntityState.Modified;
            _portalDBContext.SaveChanges();
            return data;
        }
    }
}
