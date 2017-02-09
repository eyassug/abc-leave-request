using Livit.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livit.Common.Repository
{
    public interface ITokenRepository : IRepository<Token>
    {
        void AddOrUpdate(Token t);
        Token GetByAccessToken(string accessToken);
        void RefreshToken(int id);
    }
}
