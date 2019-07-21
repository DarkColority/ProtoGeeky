using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geeky.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using Geeky.Web.Data.Entities;

    public class DataContext : DbContext
    {
        public DbSet<Evento> Eventos { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {

        }
    }
}
