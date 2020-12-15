using Microsoft.EntityFrameworkCore;
using RecipePlusApi.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Models
{
    public class RecipeContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }
      //  public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<Process> Processes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<AddRequest> Requests { get; set; }

     //   public DbSet<RequestType> RequestTypes { get; set; }

      //  public DbSet<RecipeRequest> Requests { get; set; }

        public DbSet<RecipeRequest> RecipesRequest { get; set; }

        public DbSet<IngredientRequest> IngredientsRequest { get; set; }

        public DbSet<ProcessRequest> ProcessRequests { get; set; }

        public DbSet<DeleteRequest> DeleteRequests { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<UpdateRequest> UpdateRequests { get; set; }

        public DbSet<Admin> Admins { get; set; }

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

    /*        modelBuilder.Entity<RequestType>()
                .HasKey(r => r.TypeId);*/

/*            modelBuilder.Entity<AddRequest>()
                .HasOne(r => r.RequestType)
                .WithMany(t => t.Requests)
                .HasForeignKey(r => r.TypeId);*/

            modelBuilder.Entity<IngredientRequest>()
                .HasOne(ir => ir.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(ri => ri.RecipeRequestId);

            modelBuilder.Entity<ProcessRequest>()
                .HasOne(pr => pr.Recipe)
                .WithMany(r => r.Processes)
                .HasForeignKey(rp => rp.RecipeRequestId);

            modelBuilder.Entity<AddRequest>()
                .HasOne(r => r.RecipeRequest);

            modelBuilder.Entity<DeleteRequest>()
                .HasOne(dr => dr.Recipe);

            modelBuilder.Entity<UpdateRequest>()
                .HasOne(ur => ur.Recipe)
                .WithMany(r => r.UpdateRequests)
                .HasForeignKey(ur => ur.PreviousRecipeId);

            modelBuilder.Entity<UpdateRequest>()
                .HasOne(ur => ur.RecipeRequest);
                
                


        }
    }
}
