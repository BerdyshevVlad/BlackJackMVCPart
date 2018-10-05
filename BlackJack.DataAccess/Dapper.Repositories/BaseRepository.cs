using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities;
using Dapper.Contrib.Extensions;

namespace BlackJack.DataAccess.Dapper.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

        protected readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                T itemById = await db.GetAsync<T>(id);
                await db.DeleteAsync<T>(itemById);
            }
        }

        public async Task<IEnumerable<T>> Find(Func<T, bool> predicate)           //////////////
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<T> listItem =db.GetAll<T>().Where(predicate);
                return listItem;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<T> itemList=await db.GetAllAsync<T>();
                return itemList;
            }
        }

        public async Task<T> GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
               T itemById=await db.GetAsync<T>(id);
               return itemById;
            }
        }

        public async Task Insert(T item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync<T>(item);
            }
        }

        public async Task Update(T item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.UpdateAsync<T>(item);
            }
        }
    }
}
