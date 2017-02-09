using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livit.Common.Models;
using System.Data;
using ServiceStack.OrmLite;

namespace Livit.Common.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IDbConnection _db;
        public TokenRepository(IDbConnection db)
        {
            _db = db;
        }
        public void Add(Token t)
        {
            _db.Insert(t);
        }

        public void AddOrUpdate(Token t)
        {
            var token = GetByAccessToken(t.AccessToken);
            if (token == null) // New token
                Add(t);
            else
            {
                t.Id = token.Id;
                _db.Update(t);
            }
        }

        public IEnumerable<Token> GetAll()
        {
            return _db.Select<Token>();
        }

        public Token GetByAccessToken(string accessToken)
        {
            return _db.Select<Token>(t => t.AccessToken == accessToken).FirstOrDefault();
        }

        public void RefreshToken(int id)
        {
            throw new NotImplementedException();
        }
    }
}
