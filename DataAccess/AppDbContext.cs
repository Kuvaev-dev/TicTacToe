using Microsoft.EntityFrameworkCore;
using TicTacToe.DataAccess.Models;

namespace TicTacToe.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
