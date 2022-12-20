using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data
{
    //change DbContext to IdentityDbContext<ApplicationUser> where ApplicationUser is customize class of itentity

    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        /*there is many to many rel between Movie and Actor so here we are making and seperate intity for that which 
         has actor and Movie Id as foreighn key from actor and movie entity */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(Actor_Movie => new { Actor_Movie.ActorId, Actor_Movie.MovieId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorId);

        }
        //defining model nameforeach model  public Dbset<modelname> tablename{}
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }

        //Order and OrderItem model relation in table
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        //ShoppingCartItem
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }



    }
}
