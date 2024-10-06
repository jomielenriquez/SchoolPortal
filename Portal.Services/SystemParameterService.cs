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
    public class SystemParameterService : BaseService<SystemParameter>, ISystemParameterService
    {
        private readonly ISystemParameterRepository _systemParameterRepository;
        private readonly IBaseRepository<SystemParameter> _baseRepository;
        public SystemParameterService(ISystemParameterRepository systemParameterRepository,
            IBaseRepository<SystemParameter> baseRepository) : base (baseRepository)
        {
            _systemParameterRepository = systemParameterRepository;
            _baseRepository = baseRepository;
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
