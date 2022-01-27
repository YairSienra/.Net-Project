using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Dominio;
using Aplicacion.Cursos;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{       [Route("api/[controller]")]
        [ApiController]
    public class CursoController : MiControllerBase
    {   

            [HttpGet]
            
            public async Task<ActionResult<List<CursoDto>>> Get() {

                return await Mediator.Send( new Consulta.ListaCursos());

            }
            [HttpGet("{id}")]
            public async Task<ActionResult<CursoDto>> Detalle(Guid id) 
            {
                return await Mediator.Send(new ConsultaId.CursoUnico{Id = id});
            }
            [HttpPost]
            public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data) 
            {
                  return await Mediator.Send(data);
            }
            
            [HttpPut("{id}")]
            public async Task<ActionResult<Unit>> Editar(int id, Editar.Ejecuta data)
            {
                data.CursoId = id;

                return await Mediator.Send(data);
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<Unit>> Eliminar(int id) 
            {
                return await Mediator.Send(new Eliminar.Ejecuta{CursoId = id});
            }
            
    }   
}