using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Person.Domain.Contracts;
using Person.Infrastructure.Db;

namespace Person.Infrastructure.Repositories
{
    public class Repository<TContext> : IRepository where TContext : PersonDbContext
    {
        // ReSharper disable once InconsistentNaming
        protected TContext _db;

        protected Repository(TContext context)
        {
            _db = context;
        }
        public virtual void Dispose()
        {
            _db.Dispose();
        }

        public void Reload(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                entry.Reload();
            }
        }

        public virtual int Save()
        {  
            return _db.SaveChanges(); 
        }


        public void DetachChanges()
        {
            var entries = _db.ChangeTracker.Entries().ToList();

            for (int i = 0; i < entries.Count; i++)
            {
                entries[i].State = EntityState.Detached;
            }
        }
    }
}
