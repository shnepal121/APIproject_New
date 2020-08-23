using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIproject.Controllers
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Userlogin> userlogin { get; set; }

        public DbSet<saveSearch> saveSearch { get; set; }


    }
}
