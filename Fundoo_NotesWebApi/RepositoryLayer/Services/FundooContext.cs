using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Services.Entities;
using System;

namespace RepositoryLayer.Services
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions<FundooContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        public DbSet<Label> Labels { get; set; }

        public DbSet<Collaborator> collaborators { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Label>().HasKey(c => new { c.UserId, c.NoteId });
            modelBuilder.Entity<Collaborator>().HasKey(c=> new {c.UserId, c.NoteId});
        }
    }
}
