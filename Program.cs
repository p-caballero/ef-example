namespace EfExample
{
    using EfExample.Infrastructure.Extensions;
    using EfExample.Storage;
    using Microsoft.EntityFrameworkCore;

    internal class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=ContosoPizza;Integrated Security=true;";

            connectionString.PrintConnectionString(); // Método de extensión

            var options = new DbContextOptionsBuilder<ContosoPizzaContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new ContosoPizzaContext(options);

            context.Database.EnsureCreated();

            //context.Customers.Add(new Storage.Entities.Customer()
            //{
            //    FirstName = "Luís",
            //    LastName = "García",
            //    Email = "luis.garcia@gmail.com"
            //});

            //context.SaveChanges();

            // context.Dispose(); // No es necesario por el using
        }
    }
}
