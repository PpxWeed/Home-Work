using Microsoft.EntityFrameworkCore;
using Models;
using HomeWork.Models;

namespace HomeWork.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {}
        public DbSet<Customer> Customer {get; set;}
        public DbSet<Products> Products {get; set;}
        public DbSet<Products_description> Products_description {get; set;}
        public DbSet<Customer_description> Customer_description {get; set;}
        public DbSet<Order> Order {get; set;}

        public DbSet<Reservation> Reservation {get; set;}
    }
}