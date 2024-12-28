﻿using Microsoft.EntityFrameworkCore;
using PFA.Models;

namespace PFA.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Admin> Admins { get; set; }
    }
}
