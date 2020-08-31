using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Repository.InventoryIO.Interface
{
    public interface IInventoryIORepository<T>
    {
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T Insert(T item);
        T Update2(T item);
        bool Delete(Expression<Func<T, bool>> predicate);
        DataTable ExecuteSPReturnTable(string commandString, bool IsStoredProc, params object[] param);
        bool RemoveRange(Expression<Func<T, bool>> predicate);
    }
}
