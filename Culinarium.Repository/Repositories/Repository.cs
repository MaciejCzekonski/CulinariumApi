using Culinarium.Data.DbModels;
using Culinarium.Repository.Interfaces;
using Culinarium.Repository.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Culinarium.Repository.Repositories
{
    public class Repository<T>: IRepository<T> where T:Entity
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;
        string errorMssg = string.Empty;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach(var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public IEnumerable<T> GetAllBy(Expression<Func<T, bool>> getBy, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach(var include in includes)
            {
                query = query.Include(include);
            }
            var result = query.Where(getBy);
            return result;
        }

        public T GetBy(Expression<Func<T, bool>> getBy, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach(var include in includes)
            {
                query = query.Include(include);
            }
            var result = query.FirstOrDefault(getBy);
            return result;
        }

        public bool Exist(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public int Insert(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.ModifiedDate = entity.CreationDate = DateTime.Now;
            _dbSet.Add(entity);
            return _context.SaveChanges();
        }

        public int Update(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
            return _context.SaveChanges();
        }

        public int Delete(Expression<Func<T,bool>> expression)
        {
            var entity = _dbSet.SingleOrDefault(expression);
            if(entity == null)
            {
                throw new NullReferenceException();
            }
            _dbSet.Remove(entity);
            return _context.SaveChanges();
        }

        public void LoadRelatedCollection<TInclude>(T entity, Expression<Func<T, IEnumerable<TInclude>>> collection,
            params Expression<Func<TInclude, object>>[] includes) where TInclude : Entity
        {
            var query = _context.Entry(entity).Collection(collection).Query();

            foreach(var include in includes)
            {
                query = query.Include(include);
            }
            query.Load();
        }

        public void LoadRelatedCollectionThenInclude<TInclude, TIncluded, TThenInclude>(T entity, Expression<Func<T, IEnumerable<TInclude>>> collection,
            Expression<Func<TInclude, IEnumerable<TIncluded>>> include, Expression<Func<TIncluded, TThenInclude>> thenInclude)
            where TInclude : Entity where TThenInclude : Entity where TIncluded : Entity
        {
            _context.Entry(entity).Collection(collection).Query().Include(include).ThenInclude(thenInclude).Load();
        }
    }
}
