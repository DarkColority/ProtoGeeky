using System.Collections.Generic;
using System.Threading.Tasks;
using Geeky.Web.Data.Entities;

namespace Geeky.Web.Data
{
    public interface IRepository
    {
        void AddEvento(Evento Evento);
        bool EventoExists(int id);
        Evento GetEvento(int id);
        IEnumerable<Evento> GetEventos();
        void RemoveEvento(Evento Evento);
        Task<bool> SaveAllAsync();
        void UpdateEvento(Evento Evento);
    }
}