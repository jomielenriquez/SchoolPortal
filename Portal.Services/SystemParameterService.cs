using Microsoft.EntityFrameworkCore;
using Portal.Data.Entities;
using Portal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class SystemParameterService : IBaseService<SystemParameter>, ISystemParameterService
    {
        private readonly ISystemParameterRepository<SystemParameter> _systemParameterRepository;
        private readonly IBaseRepository<SystemParameter> _baseRepository;
        public SystemParameterService(ISystemParameterRepository<SystemParameter> systemParameterRepository,
            IBaseRepository<SystemParameter> baseRepository)
        {
            _systemParameterRepository = systemParameterRepository;
            _baseRepository = baseRepository;
        }
        public int DeleteWithIds(Guid[] id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SystemParameter> GetAll()
        {
            throw new NotImplementedException();
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
            return _baseRepository.Save(data);
        }
        public IEnumerable<SystemParameter> GetBySystemParameterType()
        {
            return this._systemParameterRepository.GetBySystemParameterType();
        }
        public SystemParameter UpdateValue(Guid id, string value)
        {
            return this._systemParameterRepository.UpdateValue(id, value);
        }
    }
}
