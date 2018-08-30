using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Veiculos.Ioc.Data
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T Create();
        DbSet<E> Create<E>() where E : class, new();
        T Insert(T model);
        int Edit(T model);
        int Delete(T model);
        int Delete(Expression<Func<T, bool>> where);
        int Delete(params object[] Keys);
        T Find(params object[] Keys);
        T FindOne(Expression<Func<T, bool>> where = null);
        T FindById(int id);
        IQueryable<T> FindAll(Expression<Func<T, bool>> where = null);
        IQueryable<T> Query();
        IQueryable<T> Query(params Expression<Func<T, object>>[] includes);
        IQueryable<T> QueryFast();
        IQueryable<T> QueryFast(params Expression<Func<T, object>>[] includes);
        DbContext Context { get; }
        DbSet<T> Model { get; }
        Int32 Save();
        Task<Int32> SaveAsync();
    }
}