using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia.Migrations
{
    public class DataPrueba
    {
        public  static async Task InsertarData(CursosOnlineContext context, UserManager<Usuario> usuarioManager) {

                if (!usuarioManager.Users.Any()) {

                    var usuario = new Usuario{NombreCompleto = "Yair", UserName="YairSienra", Email="yairsienr@gmail.com"};
                  await usuarioManager.CreateAsync(usuario, "Pass1234$");
                }

        }

       
    }
}