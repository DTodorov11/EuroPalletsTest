using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.DAO
{
    public interface IDao<T>
    {
        ICollection<T> FindAll();
        T FindById(int id);
        T Add(T instance);
        void Save();
        void DeleteById(int id);
        void Delete(T instance);
        bool HasRecords();
        T EnableModifingProperty(T instance, string propertyName);
    }
}
