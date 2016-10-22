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
        public string Descripcion { get; set; }        
        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }
    public class Periodico
    {
        public int Id { get; set; }
        [Required]
        public string Imagen { get; set; }
        public string Descripcion { get; set; }

    }
}