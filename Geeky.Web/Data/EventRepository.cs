namespace Geeky.Web.Data
{
    using Entities;

    public class EventRepository : GenericRepository<Evento>, IEventRepository
    {
        public EventRepository(DataContext context) : base(context)
        {
        }
    }

}
