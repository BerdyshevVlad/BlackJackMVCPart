﻿using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities;
using Dapper.Contrib.Extensions;

namespace BlackJack.DataAccess.Dapper.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<bool> IsExist()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                bool isExist = db.GetAll<Card>().Any();
                return isExist;
            }
        }
    }
}
