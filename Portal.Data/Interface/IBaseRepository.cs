using Portal.Data.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data.Interface
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithOptions(PageModel pageModel);
        int GetCountWithOptions(PageModel pageModel);
        int DeleteWithIds(Guid[] id);
        T Save(T data);
        T GetWithId(Guid id);
        T Update(T data);
    }
}
