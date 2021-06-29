using System;
using System.ComponentModel.DataAnnotations;

namespace Hogwarts.School.Entity
{
    public class Students
    {
        /** Registros necesarios para la inscripción en la
         *  escuaela de Magia de Hogwarts
         */ 
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public string Casa { get; set; }

        public Students() { }
    }
}
