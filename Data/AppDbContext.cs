using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMaster.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<AutorModel> Autores { get; set; }
        public DbSet<LivroModel> Livros { get; set; }

    }
}