using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public abstract class DataServiceBase
    {
        private IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> _unitOfWork;

        protected IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> UnitOfWork { get { return this._unitOfWork; } }

        public DataServiceBase(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
    }
}
