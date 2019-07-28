namespace Geeky.Web.data
{
    using Geeky.Web.Data;
    using Geeky.Web.Data.Entities;
    using Geeky.Web.Helpers;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;


        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;

        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("juantorom@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    Name = "Juan",
                    LastName = "Toro",
                    Email = "juantorom@gmail.com",
                    UserName = "juantorom@gmail.com",
                    PhoneNumber = "3192983435"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

            }
            if (!this.context.Eventos.Any())
            {
                this.AddEvent("Fiesta Freaky", "Parque de los deseos", "Anime", "Es un evento muy chido","~/img1", user);

                await this.context.SaveChangesAsync();
            }
        }

        private void AddEvent(string name, string direccion, string tematica, string descripcion, string imagen, User user)
        {
            this.context.Eventos.Add(new Evento
            {
                Nombre = name,
                Direccion = direccion,
                Tematica = tematica,
                Descripcion = descripcion,
                ImageURL = imagen,
                User = user
            });
        }
    }
}
