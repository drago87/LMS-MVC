﻿using Awesome.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awesome.Data
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        public EFRepository(DbContext ctx)
        {
            if (ctx == null)
                throw new ArgumentNullException("dbContext");
            DbContext = ctx;
            DbSet = DbContext.Set<T>();
        }

        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return;

            Delete(entity);
        }
        // Föreslagna tillägg från RP done right:
        //public void AddRange(IEnumerable<T> entities)
        //{
        //    DbContext.Set<T>().AddRange(entities);
        //}

        //public void RemoveRange(IEnumerable<T> entities)
        //{
        //    DbContext.Set<T>().RemoveRange(entities);
        //}
    }
}
