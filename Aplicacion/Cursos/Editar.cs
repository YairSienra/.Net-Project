using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
        public int CursoId {get;set;}
        public string Titulo {get;set;}
        public string Descripcion {get; set;}
        public DateTime? FechaPublicacion {get;set;}
        
        }

        private class Manejador : IRequestHandler<Ejecuta>
        {
           private readonly CursosOnlineContext _context;
           public Manejador(CursosOnlineContext context)
           {
               _context = context;
           }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso =  await _context.Curso.FindAsync(request.CursoId);

                if (curso==null)
                    {
                        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {curso = "No se encontro el curso"});
                       
                    }

                    curso.Titulo = request.Titulo ?? curso.Titulo;
                    curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                    curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

                var resultado = await _context.SaveChangesAsync();

                if (resultado>0)
                
                    return Unit.Value;   
                      throw new Exception("No se Guardaron los cambios");
                
            }
        }

    }
}