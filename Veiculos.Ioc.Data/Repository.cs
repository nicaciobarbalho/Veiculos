using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using Veiculos.Ioc.Core;
using System.Data.Entity.Core;

namespace Veiculos.Ioc.Data
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        DbSet<T> model;
        public Repository()
        {
            this.Context = Activator.CreateInstance<IocDbContext>();
            this.Model = this.Context.Set<T>();
          
        }
        public Repository(DbContext Context)
        {
            this.Context = Context;
            this.Model = this.Context.Set<T>();
         
        }
        public T Insert(T model)
        {
            this.Model.Add(model);
            this.Save();
            return model;
        }
        public int Edit(T model)
        {
            this.Context.Entry<T>(model).State = EntityState.Modified;
            
            return this.Save();
        }
        public int Delete(T model)
        {
            this.Context.Entry<T>(model).State = EntityState.Deleted;           
            return this.Save();
        }
        public int Delete(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            T model = this.Model.Where<T>(where).FirstOrDefault<T>();
            if (model != null)
            {
                return Delete(model);
            }
            return 0;
        }
        public int Delete(params object[] Keys)
        {
            T model = this.Model.Find(Keys);
            if (model != null)
            {
                return Delete(model);
            }
            return 0;
        }
        public T Find(params object[] Keys)
        {
            return this.Model.Find(Keys);
        }
        public  T FindById(int id)
        {
            /*dynamic obj = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
            obj.Id = id;
            object o = (T)Convert.ChangeType(obj, typeof(T));
            object f = this.Model.Find(id);*/
            return this.Model.Find(id);
        
        }
        public virtual T FindOne(Expression<Func<T, bool>> where = null)
        {         
            return FindAll(where).FirstOrDefault();
        }
        public virtual IQueryable<T> FindAll(Expression<Func<T, bool>> where = null)
        {
            return null != where ? Context.Set<T>().Where(where) : Context.Set<T>();
        }
        public IQueryable<T> Query()
        {
            return this.Model;
        }
        public IQueryable<T> Query(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> Set = this.Query();
            foreach (var include in includes)
            {
                Set = Set.Include(include);
            }
            return Set;
        }
        public IQueryable<T> QueryFast()
        {
            return this.Model.AsNoTracking<T>();
        }
        public IQueryable<T> QueryFast(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> Set = this.QueryFast();
            foreach (var include in includes)
            {
                Set = Set.Include(include);
            }
            return Set.AsNoTracking();

        }
        public T Create()
        {
            return this.Model.Create();
        }
        public DbSet<E> Create<E>()
            where E : class, new()
        {
            return this.Context.Set<E>();
        }
        public DbContext Context
        {
            get;
            private set;
        }
        public DbSet<T> Model
        {
            get
            {
                if (model == null)
                {
                    model = Context.Set<T>();
                }
                return model;
            }
            private set { model = value; }
        }
        public void Dispose()
        {
            if (this.Context != null)
            {
                this.Context.Dispose();
            }
            GC.SuppressFinalize(this);
        }
        public int Save()
        {
            return this.Context.SaveChanges();
        }
        public async System.Threading.Tasks.Task<int> SaveAsync()
        {
            return await this.Context.SaveChangesAsync();
        }
    }
}