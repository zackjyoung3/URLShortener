using System;
using Microsoft.EntityFrameworkCore;
using URLShortener.Services.Models;
using Microsoft.Extensions.Options;

namespace URLShortener.Services
{
    public class MyContext : DbContext
    {
        public DbSet<URL> URL { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=url_shortener;user=root;password=Cookie!23");
        }
    }
}
