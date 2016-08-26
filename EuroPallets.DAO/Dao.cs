using EuroPallets.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.DAO
{
    public class Dao<T> : DisposableDao, IDao<T> where T : class
    {
        public Dao(EuroPalletsDbContext context) : base(context) { }

        internal virtual DbSet<T> EntitySet
        {
            get
            {
                return DB.Set<T>();
            }
        }

        public ICollection<T> FindAll()
        {
            return EntitySet.ToList();
        }

        public ICollection<T> FindAllWhere(Expression<Func<T, bool>> whereSelector)
        {
            ICollection<T> elements = EntitySet.Where(whereSelector).ToList();

            return elements;
        }

        public IEnumerable<T> FindWithPagination<TSort>(
            Expression<Func<T, bool>> whereSelector,
            Expression<Func<T, TSort>> orderBySelector,
            int startRowIndex,
            int maximumRows,
            out int totalRowCount)
        {
            ICollection<T> elements = EntitySet.Where(whereSelector).OrderBy(orderBySelector).Skip(startRowIndex).Take(maximumRows).ToList();

            //totalRowCount = EntitySet.Count();

            ICollection<T> allElements = EntitySet.Where(whereSelector).ToList();

            totalRowCount = allElements.Count();


            return elements;
        }

        public T FindById(int id)
        {
            return EntitySet.Find(id);
        }

        public T Add(T instance)
        {
            T resultInstance = EntitySet.Add(instance);
            DB.SaveChanges();
            return resultInstance;
        }

        public IEnumerable<T> AddAll(IEnumerable<T> instances)
        {
            IEnumerable<T> resultInstance = EntitySet.AddRange(instances);
            DB.SaveChanges();
            return resultInstance;
        }

        public void Save()
        {
            DB.SaveChanges();
        }

        public void DeleteById(int id)
        {
            T instance = FindById(id);
            Delete(instance);
        }

        public void Delete(T instance)
        {
            EntitySet.Remove(instance);
            DB.SaveChanges();
        }

        public void Update(T instance)
        {
            DB.Entry(instance).State = EntityState.Modified;
            DB.SaveChanges();
        }

        public T EnableModifingProperty(T instance, string propertyName)
        {
            DB.Entry(instance).Property(propertyName).IsModified = true;

            return instance;
        }

        public bool HasRecords()
        {
            return EntitySet.Any();
        }

        public bool HasRecordsExpression(Expression<Func<T, bool>> whereSelector)
        {
            return EntitySet.Where(whereSelector).Any();
        }
    }
}
