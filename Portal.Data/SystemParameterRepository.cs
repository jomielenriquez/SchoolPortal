using Microsoft.EntityFrameworkCore;
using Portal.Data.Data;
using Portal.Data.Entities;
using Portal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Portal.Data
{
    public class SystemParameterRepository : IBaseRepository<SystemParameter>, ISystemParameterRepository<SystemParameter>
    {
        private readonly PortalDBContext _dbContext;
        public SystemParameterRepository(PortalDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int DeleteWithIds(Guid[] id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SystemParameter> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SystemParameter> GetBySystemParameterType()
        {
            return _dbContext.SystemParameter.Include(x => x.SystemParameterType)
                .Where(a => a.SystemParameterTypeId == new Guid("2D3C3723-DEE9-4FA4-91A6-A06ED7010861")).ToList();
        }

        public SystemParameter GetWithId(Guid id)
        {
            throw new NotImplementedException();
        }

        public SystemParameter Save(SystemParameter data)
        {
            throw new NotImplementedException();
        }

        public SystemParameter Update(SystemParameter data)
        {
            var parameterToUpdate = _dbContext.SystemParameter.FirstOrDefault(p => p.Id == data.Id);
            if (parameterToUpdate != null)
            {
                parameterToUpdate = data;
                _dbContext.SaveChanges();
            }
            return _dbContext.SystemParameter.FirstOrDefault(p => p.Id == data.Id);
        }
        public SystemParameter UpdateValue(Guid id, string value)
        {
            var parameterToUpdate = _dbContext.SystemParameter.FirstOrDefault(p => p.Id == id);
            if (parameterToUpdate != null)
            {
                parameterToUpdate.Value = value;
                _dbContext.SaveChanges();
            }
            return _dbContext.SystemParameter.FirstOrDefault(p => p.Id == id);
        }
    }
}
