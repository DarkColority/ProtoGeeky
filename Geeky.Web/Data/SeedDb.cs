namespace Geeky.Web.data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Geeky.Web.Data;
    using Geeky.Web.Data.Entities;
    using Geeky.Web.Models;

    public class SeedDb
    {
        private readonly DataContext context;
        private Random random;

        public SeedDb(DataContext context)
        {
            this.context = context;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Eventos.Any())
            {
                this.AddEvent("Fiesta Freaky", "Parque de los deseos", "Anime");

                await this.context.SaveChangesAsync();
            }
        }

        private void AddEvent(string name, string direccion, string tematica)
        {
            this.context.Eventos.Add(new Evento
            {
                Nombre = name,
                Direccion = direccion,
                Tematica = tematica,
                
            });
        }
    }
}