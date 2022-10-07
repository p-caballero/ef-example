namespace EfExample
{
    using EfExample.Infrastructure.Extensions;
    using EfExample.Storage;
    using EfExample.Storage.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    internal class Program
    {
        static void Main(string[] args)
        {
            //const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=ContosoPizza;Integrated Security=true;"; // SQL Server LocalDB
            const string connectionString = "Data Source=.;Database=ContosoPizza;Integrated Security=true;"; // SQL Server

            connectionString.PrintConnectionString(); // Método de extensión

            var options = new DbContextOptionsBuilder<ContosoPizzaContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new ContosoPizzaContext(options);

            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            AddCustomer(context);
            AddProduct(context);

            var allProducts = context.Products.ToList();

            var firstProduct = context.Products.FirstOrDefault(x => x.OrderDetails.Count == 2);

            var firstProductWithOrdedrDetail = context.Products.Include(x => x.OrderDetails)
                .Where(x => x.Price > 10)
                .Where(x => x.OrderDetails.Count == 2)
                .FirstOrDefault();

            // context.Dispose(); // No es necesario por el using
        }

        private static void AddProduct(ContosoPizzaContext context)
        {
            // Crea order donde CustomerId es 1 y OrderPlaced ahora
            var order = new Order()
            {
                CustomerId = 1,
                OrderPlaced = DateTime.Now
            };

            context.Orders.Add(order); // Lo añade a la tabla sin guardar (en memoria)

            context.SaveChanges(); // Guarda

            // Crea un objeto de tipo Product
            var product1 = new Product()
            {
                Name = $"Zapatillas {DateTime.Now}",
                Price = 100
            };

            // Crea un objeto de tipo OrderDetail
            var orderDetail1 = new OrderDetail()
            {
                Quantity = Random.Shared.Next(1, 100),
                Order = order
            };

            // Crea un objeto de tipo OrderDetail
            var orderDetail2 = new OrderDetail()
            {
                Quantity = Random.Shared.Next(1, 100),
                Order = order
            };

            // Añade las 2 OrderDetail en memoria
            product1.OrderDetails.Add(orderDetail1);
            product1.OrderDetails.Add(orderDetail2);

            // Añade product1 en memoria
            context.Products.Add(product1);

            // Guarda todo lo que está pendiente
            context.SaveChanges();
        }

        private static void AddCustomer(ContosoPizzaContext context)
        {
            var customer1 = new Customer()
            {
                FirstName = "Luís",
                LastName = "García",
                Email = "luis.garcia@gmail.com"
            };

            context.Customers.Add(customer1);
        }
    }
}
