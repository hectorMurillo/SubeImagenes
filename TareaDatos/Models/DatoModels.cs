using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TareaDatos.Models
{
    public class Bache
    {
        public int Id { get; set; }
        [Required]
        public string Imagen { get; set; }
        public string Pregunta1 { get; set; }
        [Required]
        public string Respuesta1 { get; set; }
        public string Pregunta2 { get; set; }
        [Required]
        public string Respuesta2 { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string  Fecha { get; set; }
    }
    public class Periodico
    {
        public int Id { get; set; }
        [Required]
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public string Pregunta1 { get; set; }
        public string Respuesta1 { get; set; }
        public string Pregunta2 { get; set; }
        public string Respuesta2 { get; set; }
        public string  Fecha { get; set; }

    }
}