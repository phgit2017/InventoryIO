using DataAccess.Entities.Context.Interface;
using DataAccess.Repository.InventoryIO.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Repository.InventoryIO
{
    public class InventoryIORepository <T> : IInventoryIORepository<T> where T : class
    {
        protected IInventoryIOEntities Db;

        public InventoryIORepository(IInventoryIOEntities dataContext)
        {
            Db = dataContext;
        }

        #region IRepository<T> Members

        public T Insert(T item)
        {
            Db.Set<T>().Add(item);
            Db.SaveChanges();
            return item;
        }

        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            var list = SearchFor(predicate).ToList();
            if (list != null)
            {
                foreach (T ctr in list)
                {
                    Db.Entry<T>(ctr).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }

                Db.SaveChanges();
                return true;
            }

            return false;
        }

        public T Update2(T item)
        {
            try
            {
                Db.Set<T>().Attach(item);
                Db.Entry<T>(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Db.SaveChanges();

                return item;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return Db.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return Db.Set<T>();
        }

        public bool RemoveRange(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var list = SearchFor(predicate);

                Db.Set<T>().RemoveRange(list);

                Db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public DataTable ExecuteSPReturnTable(string commandString, bool IsStoredProc, params object[] param)
        {

            DataTable dt = new DataTable();

            using (SqlConnection conn =
                new SqlConnection("Data Source=localhost\\MSSQLSERVER2016;initial catalog=ChainSawDBV3;persist security info=True;user id=sa;password=sa;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                conn.Open();
                // Create an EntityCommand.
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = commandString;

                    if (IsStoredProc)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        cmd.CommandType = CommandType.Text;
                    }

                    if (param != null)
                    {
                        foreach (var p in param)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }


                    // Execute the command.
                    using (SqlDataReader rdr =
                        cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        // Read the results returned by the stored procedure.
                        dt.Load(rdr);
                    }
                }

                conn.Close();
            }


            return dt;
        }

        #endregion
    }
}
