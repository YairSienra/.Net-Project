using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using Microsoft.IdentityModel.Tokens;

namespace Seguridad.TokenSeguridad
{
    public class JwtGenerador : IJwtGenerador
    {
        public string CrearToken(Usuario usuario)
        {
             var claims = new List<Claim>{
                 new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
             }; 


             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra Secreta"));
             var credecnciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

             var tokenDesciption = new SecurityTokenDescriptor {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(30),
                    SigningCredentials = credecnciales
             };

            var tokenManjeador = new JwtSecurityTokenHandler();
            var token = tokenManjeador.CreateToken(tokenDesciption);


            return tokenManjeador.WriteToken(token);
        }
    }
}