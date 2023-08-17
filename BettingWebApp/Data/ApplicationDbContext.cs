using BettingWebApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace BettingWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Bet> Bets { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Odd> Odds { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Bet>().HasData(
        //        new Bet()
        //        {

        //        });
        //}


    }

}
