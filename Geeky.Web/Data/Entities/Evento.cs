namespace Geeky.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;


    public class Evento : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre del evento")]
        public string Nombre { get; set; }


        [Required]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required]
        public string Tematica { get; set; }

        [Required]
        public string Descripcion { get; set; }


        public User User { get; set; }
        
    }
}
