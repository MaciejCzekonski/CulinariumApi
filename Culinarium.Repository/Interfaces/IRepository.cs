using Culinarium.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Culinarium.Repository.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetAllBy(Expression<Func<T, bool>> getBy, params Expression<Func<T, object>>[] includes);
        T GetBy(Expression<Func<T, bool>> getBy, params Expression<Func<T, object>>[] includes);
        bool Exist(Expression<Func<T, bool>> expression);
        int Insert(T entity);
        int Update(T entity);
        int Delete(Expression<Func<T, bool>> expression);
        void LoadRelatedCollection<TInclude>(T entity, Expression<Func<T, IEnumerable<TInclude>>> collection,
            params Expression<Func<TInclude, object>>[] includes) where TInclude : Entity;
        void LoadRelatedCollectionThenInclude<TInclude, TIncluded, TThenInclude>(T entity, Expression<Func<T, IEnumerable<TInclude>>> collection,
            Expression<Func<TInclude, IEnumerable<TIncluded>>> include, Expression<Func<TIncluded, TThenInclude>> thenInclude)
            where TInclude : Entity where TThenInclude : Entity where TIncluded : Entity;
    }
}
