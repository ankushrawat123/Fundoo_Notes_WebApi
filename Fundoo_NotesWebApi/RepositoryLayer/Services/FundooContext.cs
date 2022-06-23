using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Services.Entities;
using System;

namespace RepositoryLayer
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions<FundooContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
