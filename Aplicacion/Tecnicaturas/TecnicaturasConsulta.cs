using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;


namespace Aplicacion.Tecnicaturas
{
    public class TecnicaturasConsulta
    {
        public class Ejecuta : IRequest
        {
            public string Nombre {get; set;}
            public string Descripcion {get; set;}
            public int Vacantes {get;set;}
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;
            public Manejador (CursosOnlineContext context) {

                    _context = context;

            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var tecnicatura = new Tecnicatura {
                        Nombre = request.Nombre,
                        Descripcion = request.Descripcion,
                        Vacantes = request.Vacantes
                };

                _context.Tecnicatura.Add(tecnicatura);
                var valor = await _context.SaveChangesAsync();
                if (valor> 0){
                    return Unit.Value;
                }
                {
                    throw new Exception("No se pudo agregar");
                }
            }
        }
    }
}