using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livit.Common.Models
{
    public class Token
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string IdToken { get; set; }
        public DateTime Issued { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }
        public string TokenType { get; set; }
    }
}
