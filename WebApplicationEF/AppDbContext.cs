﻿using Microsoft.EntityFrameworkCore;

namespace WebApplicationEF;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
}