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
    public class SystemParameterRepository : BaseRepository<SystemParameter>, ISystemParameterRepository
    {
        public SystemParameterRepository(PortalDBContext portalDBContext) : base(portalDBContext) { }
        
        public IEnumerable<SystemParameter> GetBySystemParameterType()
        {
            return _portalDBContext.SystemParameter.Include(x => x.SystemParameterType)
                .Where(a => a.SystemParameterTypeId == new Guid("2D3C3723-DEE9-4FA4-91A6-A06ED7010861")).ToList();
        }
        public SystemParameter UpdateValue(Guid id, string value)
        {
            var parameterToUpdate = _portalDBContext.SystemParameter.FirstOrDefault(p => p.Id == id);
            if (parameterToUpdate != null)
            {
                parameterToUpdate.Value = value;
                _portalDBContext.SaveChanges();
            }
            return _portalDBContext.SystemParameter.FirstOrDefault(p => p.Id == id);
        }
    }
}
