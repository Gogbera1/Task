using Application.AppConfigs;
using Application.Jwt.Request;
using Application.Jwt.Service;
using Common.Enums;
using Common.Resultwrapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Jwt.ServiceImpl
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings _tokenSettings;

        public TokenService(IOptions<TokenSettings> tokenSettings)
        {
            _tokenSettings = tokenSettings.Value ?? throw new ArgumentNullException(nameof(tokenSettings));
        }

        public ResultWrapper<TokenModel> GenerateToken(string id, IDictionary<string, string> claims, List<string> permissions)
        {
            if (string.IsNullOrWhiteSpace(id))
                return new ResultWrapper<TokenModel>
                {
                    Status = ResultCodeEnum.Code404UserNotFound
                };

            var convertedClaims = claims?.Select(x => new Claim(x.Key, x.Value)).ToList() ?? new List<Claim>();
            convertedClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, id));
            if (permissions?.FirstOrDefault() != null)
            {
                permissions.ForEach(p =>
                {
                    convertedClaims.Add(new Claim("permission", p));
                });
            }

            var accessToken = GenerateJwt(convertedClaims);
            var tokenResponse = new TokenModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(accessToken)
            };

            return new ResultWrapper<TokenModel>
            {
                Status = ResultCodeEnum.Code200Success,
                Value = tokenResponse
            };
        }

        private JwtSecurityToken GenerateJwt(List<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
