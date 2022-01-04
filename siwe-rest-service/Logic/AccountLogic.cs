using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Nethereum.Siwe.Core;

using siwe_rest_service.Models;

namespace siwe_rest_service.Logic
{
    public class TokenLogic : ITokenLogic
    {
        private readonly TokenSettings _tokenSettings;

        public TokenLogic(IOptions<TokenSettings> tokenSettings)
        {
            _tokenSettings = tokenSettings.Value;
        }

        public string GetAuthenticationToken(SiweMessage loginMsg)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new List<Claim>();

            if (loginMsg.Resources != null)
            {
                for (int resourceCount = 1; resourceCount <= loginMsg.Resources.Count; ++resourceCount)
                {
                    var tmpResource = loginMsg.Resources[resourceCount - 1];
                       
                    if (!String.IsNullOrEmpty(tmpResource))
                        userClaims.Add(new Claim("Resource" + resourceCount, tmpResource));
		        }
            }

            var jwtToken = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                expires: Extensions.ConvertToDateTime(loginMsg.ExpirationTime),
                signingCredentials: credentials,
                claims: userClaims
            );
 
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}
