using Portal.Data;
using Portal.Data.Interface;
using Portal.Data.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IBaseRepository<T> _repository;
        public BaseService(IBaseRepository<T> repository) { 
            _repository = repository;
        }
        public int DeleteWithIds(Guid[] id)
        {
            return _repository.DeleteWithIds(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<T> GetAllWithOptions(PageModel pageModel)
        {
            return _repository.GetAllWithOptions(pageModel);
        }

        public int GetCountWithOptions(PageModel pageModel)
        {
            return _repository.GetCountWithOptions(pageModel);
        }

        public T GetWithId(Guid id)
        {
            return _repository.GetWithId(id);
        }

        public T Save(T data)
        {
            return _repository.Save(data);
        }

        public T Update(T data)
        {
            return _repository.Update(data);
        }
    }
}
