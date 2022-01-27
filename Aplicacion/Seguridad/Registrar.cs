using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class Registrar 
    {
            public class Ejecuta : IRequest<UsuarioData>
            {
                public string Nombre {get; set;}
                public string Apellidos {get; set;}
                public string Email {get; set;}
                public string Password {get; set;}
                public string username {get; set;}
            }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly CursosOnlineContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;

            public Manejador (CursosOnlineContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador) {

                    _context = context;
                    _userManager = userManager;
                    _jwtGenerador = jwtGenerador;

            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                 var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                 if (existe)
                 {
                     throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new {mensaje = "Existe esa cuenta ya"});
                 }

                 var Usuario = new Usuario {
                     NombreCompleto = request.Nombre + " " + request.Apellidos,
                    Email = request.Email,
                    UserName = request.username
                 };

            var resultado = await _userManager.CreateAsync(Usuario, request.Password);

                  if (resultado.Succeeded)
                  {
                      return new UsuarioData{
                          NombreCompleto = Usuario.NombreCompleto,
                          Token = _jwtGenerador.CrearToken(Usuario),
                          Username = Usuario.UserName,
                      };
                  }

                  throw new Exception("No se pudo agregar al nuevo usuario");
            }
        }
    }
}