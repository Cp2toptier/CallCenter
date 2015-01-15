using CallCenter.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CallCenter.Application
{
    public class Manager<T>
        where T: class
    {
        public Manager(DBContext context)
        {
            Context = context;
        }

        public DBContext Context { get; private set; }

        public T Add(T entity)
        {
            return Context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public T Get(Guid id)
        {
            return Context.Set<T>().Find(id);
        }

        public T Remove(T entity)
        {
            return Context.Set<T>().Remove(entity);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
