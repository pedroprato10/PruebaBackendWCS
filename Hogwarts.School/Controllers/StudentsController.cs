using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hogwarts.School.Entity;
using Hogwarts.School.Validation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hogwarts.School.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        //Contexto de la base de datos
        private readonly StudentsContext context;
        // Longitud máxima de la identificació4n
        const long MAX = 10000000000;

        public StudentsController(StudentsContext context)
        {
            this.context = context;
        }


    
        [HttpGet] //"api/students"
        // Obtener los registro de la base de datos
        public ActionResult<IEnumerable<Students>> Get()
        {
            return Ok(context.students.ToList());
        }



        [HttpGet] //"api/students/id"
        [Route("{id}")]
        // Obetner el registro de un usuario existente
        public ActionResult GetStudent(long id)
        {
            // Se verifica la estructura del JSON
            if (!ModelState.IsValid)
                return BadRequest("Información erronea");

            //Se busca la identificación en los registros actuales de la DB
            var StudentsDb = context.students.Where(index => index.Id == id)
                .FirstOrDefault();

            if (StudentsDb == null)
                // No se encontró el registro de la base de datos
                return BadRequest("No existe la identificación");

            return Ok(StudentsDb);

        }



        [HttpPost] //"api/students"
        // Almacenarun usuario en la base de datos
        public ActionResult Post([FromBody] Students student)
        {
            
            AnswerValidation answerValidation = new AnswerValidation();
            StudentValidation studentValidation = new StudentValidation();

            // Se verifica la estructura del JSON
            if (!ModelState.IsValid)
                return BadRequest("Datos incompletos");

            //Se busca la identificación en los registros actuales de la DB
            var StudentsDb = context.students.Where(index => index.Id == student.Id)
                .FirstOrDefault();

            // Si la identificación ya existe se procede a dar error
            if (StudentsDb != null)
                return BadRequest("El registro ya existe. No puede re-escribirlo");

            if (student.Id <= 0 || student.Id > MAX)
                return BadRequest("Escriba una identificación menor a 10 dígitos");
            else
                //se hace la validadión de los datos
                answerValidation = studentValidation.Student(student);

            //Hubo un error e la validación
            if (answerValidation.codigo == 1)
                return BadRequest(answerValidation.mensaje);
                   
            context.students.Add(student);
            context.SaveChanges();
            return Ok();
        }



        [HttpPut] //"api/students"
        // Actualizar un usuario cuyo registro existe
        public ActionResult Put ([FromBody] Students student)
        {
            AnswerValidation answerValidation = new AnswerValidation();
            StudentValidation studentValidation = new StudentValidation();

            // Se verifica la estructura del JSON
            if (!ModelState.IsValid)
                return BadRequest("Datos incompletos");

            // Se valida que la identificación esté en el rango
            if (student.Id <= 0 || student.Id > MAX)
                return BadRequest("Escriba una identificación menor a 10 dīgitos");

            //Se busca la identificación en los registros actuales de la DB
            var StudentsDb = context.students.Where(index => index.Id == student.Id)
                .FirstOrDefault();

            // Si no se consigue el regsitro en la base de datos
            if (StudentsDb == null)
                return BadRequest("No se encontró la identificación suministrada");

            else
                //Se hace la validadión de los datos
                answerValidation = studentValidation.Student(student);

            if (answerValidation.codigo == 1)
                return BadRequest(answerValidation.mensaje);

            // Se actualizan los registros
            StudentsDb.Nombre = student.Nombre;
            StudentsDb.Apellido = student.Apellido;
            StudentsDb.Edad = student.Edad;
            StudentsDb.Casa = student.Casa;

            context.Update(StudentsDb);
            context.SaveChanges();

            return Ok();
        }


        [HttpDelete] //"api/students"
        [Route("{id}")]
        // Eliminar el registro de un usuario existente
        public ActionResult Delete (long id)
        {
            // Se verifica la estructura del JSON
            if (!ModelState.IsValid)
                return BadRequest("Información erronea");

            //Se busca la identificación en los registros actuales de la DB
            var StudentsDb = context.students.Where(index => index.Id == id)
                .FirstOrDefault();

            if (StudentsDb == null)
                // No se encontró el registro en la base de datos
                return BadRequest("No existe la identificación");

            context.Remove(StudentsDb);
            context.SaveChanges();

            return Ok();

        }

    }
}
