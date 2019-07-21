

namespace Geeky.Web.Data
{
    
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Evento> GetEventos()
        {
            return this.context.Eventos.OrderBy(p => p.Nombre);
        }

        public Evento GetEvento(int id)
        {
            return this.context.Eventos.Find(id);
        }

        public void AddEvento(Evento Evento)
        {
            this.context.Eventos.Add(Evento);
        }

        public void UpdateEvento(Evento Evento)
        {
            this.context.Update(Evento);
        }

        public void RemoveEvento(Evento Evento)
        {
            this.context.Eventos.Remove(Evento);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public bool EventoExists(int id)
        {
            return this.context.Eventos.Any(p => p.Id == id);
        }
    }

}

