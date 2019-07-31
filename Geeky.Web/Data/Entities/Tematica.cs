namespace Geeky.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;


    public class Tematica : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tematica")]
        public string Nombre { get; set; }       

        [Display(Name = "Imagen")]
        public string ImageURL { get; set; }


    }
}
