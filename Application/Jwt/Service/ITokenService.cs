using Application.Jwt.Request;
using Common.Resultwrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jwt.Service
{
    public interface ITokenService
    {
        ResultWrapper<TokenModel> GenerateToken(string id, IDictionary<string, string> claim, List<string> permissions);
    }
}
