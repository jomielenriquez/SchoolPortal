using Microsoft.EntityFrameworkCore;
using Portal.Data.Data;
using Portal.Data.Entities;
using Portal.Data.Interface;
using Portal.Data.SearchModel;
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

        public int GetCountWithOptions(PageModel pageModel)
        {
            // Start with all entities
            var query = _portalDBContext.Set<T>().AsQueryable();

            // Apply filtering based on the Search property if provided
            if (!string.IsNullOrEmpty(pageModel.Search))
            {
                // Assuming that the entities have a Name or similar property to filter on
                // We need to use reflection to filter based on the property name dynamically.
                var property = typeof(T).GetProperty(pageModel.OrderByProperty);

                if (property != null)
                {
                    // Perform a case-insensitive search on the specified property
                    query = query.Where(e => EF.Property<string>(e, property.Name).Contains(pageModel.Search));
                }
            }

            // Count the total number of records after filtering
            return query.Count();
        }
        public IEnumerable<T> GetAllWithOptions(PageModel pageModel)
        {
            // Start with all entities
            var query = _portalDBContext.Set<T>().AsQueryable();

            // Apply filtering based on the Search property if provided
            if (!string.IsNullOrEmpty(pageModel.Search))
            {
                // Assuming entities have a property to search on, apply filtering
                var property = typeof(T).GetProperty(pageModel.OrderByProperty);

                if (property != null)
                {
                    // Perform a case-insensitive search on the specified property
                    query = query.Where(e => EF.Property<string>(e, property.Name).Contains(pageModel.Search));
                }
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(pageModel.OrderByProperty))
            {
                if (pageModel.IsAscending)
                {
                    query = query.OrderBy(e => EF.Property<object>(e, pageModel.OrderByProperty));
                }
                else
                {
                    query = query.OrderByDescending(e => EF.Property<object>(e, pageModel.OrderByProperty));
                }
            }

            // Apply paging
            query = query.Skip((pageModel.Page - 1) * pageModel.PageSize)
                         .Take(pageModel.PageSize);

            return query.ToList();
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
