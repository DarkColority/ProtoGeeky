
namespace Geeky.Web.Models
{
    using Geeky.Web.Data.Entities;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class EventViewModel : Evento
    {
        [Display(Name = "Imagen")]
        public IFormFile ImageFile { get; set; }
    }
}
