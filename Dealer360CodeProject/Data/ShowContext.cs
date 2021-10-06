using Microsoft.EntityFrameworkCore;
using Dealer360CodeProject.Models;

namespace Dealer360CodeProject.Data
{
    public class ShowContext : DbContext
    {
        public DbSet<Show> Shows { get; set; }
        public ShowContext(DbContextOptions<ShowContext> options) : base(options)
        {
        }
    }
}
