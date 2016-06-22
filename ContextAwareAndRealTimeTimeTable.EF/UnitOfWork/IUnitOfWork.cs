using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.Repository;
using ContextAwareAndRealTimeTimeTable.EF.Context;

namespace ContextAwareAndRealTimeTimeTable.EF.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : IDbContext
    {
        IRepository<TEntity> Get<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
