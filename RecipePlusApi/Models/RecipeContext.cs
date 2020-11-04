using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlus.Models
{
    public class RecipeContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<User> Users { get; set; }

        public RecipeContext(DbContextOptions<RecipeContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>()
            .HasKey(t => new { t.RecipeId, t.IngredientId, });

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(pt => pt.Recipe)
                .WithMany(p => p.RecipeIngredients)
                .HasForeignKey(pt => pt.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(pt => pt.Ingredient)
                .WithMany(t => t.RecipeIngredients)
                .HasForeignKey(pt => pt.IngredientId);

            modelBuilder.Entity<Process>()
                .HasOne(p => p.Recipe)
                .WithMany(r => r.Processes)
                .HasForeignKey(r => r.RecipeId);

            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.UserId);

        }
    }
}
