using System;
using Hogwarts.School.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Hogwarts.School.Validation
{
    public class StudentValidation
    {
        public AnswerValidation Student(Students student)
        {
            AnswerValidation answerValidation = new AnswerValidation();
            answerValidation.codigo = 0;

            // Se Valida cada registro
            string Answer;
            Answer = NameValidation(student.Nombre);
            Answer += SurNameValidation(student.Apellido);
            Answer += AgeValidation(student.Edad);
            Answer += HouseValidation(student.Casa);

            if (Answer == "")
            {
                return answerValidation;
            }
            //Hubo un error en alguno de los datos
            answerValidation.codigo = 1;
            answerValidation.mensaje = Answer;

            return answerValidation;
        }
        
        /** Se valida el nombre */
        private string NameValidation (string Name)
        {
            if (Name.Length > 20)
            {
                return "Nombre muy largo\n";
            }
            return "";
        }

        /** Se valida el Apellido */
        private string SurNameValidation (string Surname)
        {
            if (Surname.Length > 20)
                return "Apellido muy largo\n";
            return "";
        }

        /** Se valida la edad */
        private string AgeValidation (int Age)
        {
            if (Age <= 0)
                return "Escriba un numero positivo\n";
            if (Age > 99)
                return "La edad máxima es 99 años\n";
            return "";
        }

        /** Se valida la casa */
        private string HouseValidation (string House)
        {
            if (House.ToLower() == "gryffindor")
                return "";
            if (House.ToLower() == "hufflepuff")
                return "";
            if (House.ToLower() == "ravenclaw")
                return "";
            if (House.ToLower() == "slytherin")
                return "";
            

            return "Eliga una casa correcta";
        }
    }
}
