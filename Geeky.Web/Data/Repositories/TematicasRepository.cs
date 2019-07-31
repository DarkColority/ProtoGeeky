namespace Geeky.Web.Data.Repositories
{
    using Geeky.Web.Data.Entities;
    public class TematicasRepository : GenericRepository<Tematica>, ITematicasRepository
    {
        public TematicasRepository(DataContext context) : base(context)
        {
        }
    }
}
