using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Dominio;
using System.Threading;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Aplicacion.Cursos
{
    public class NewBaseType
    {
    }

    public class Consulta
    {
        public class ListaCursos : IRequest<List<CursoDto>> { }



        public class Manejador : IRequestHandler<ListaCursos, List<CursoDto>>
        {

            private readonly CursosOnlineContext _context;

            private readonly IMapper _mapper;
                public Manejador (CursosOnlineContext context, IMapper mapper) {

                    _context = context;
                    _mapper = mapper;

                }
            public async Task<List<CursoDto>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                var cursos = await _context.Curso.Include(x => x.InstuctoresLink).ThenInclude(x => x.Instructor)
                .ToListAsync();

                var cursosDto = _mapper.Map<List<Curso>,List<CursoDto>>(cursos);
                    return cursosDto;
            
        }


            
        }
    }

}