using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Dominio
{
    public class Instructor 
    {
        public Guid InstructorId {get; set;}
        public string Nombre {get; set;}
        public string Apellido {get; set;}
        public string Grade {get; set;}
        public byte[] FotoPerfil {get;set;}


        public ICollection<CursoInstructor> CursoLink {get;set;}
    }
}