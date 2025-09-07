using Dota2WinPredictor.Models;
using Microsoft.EntityFrameworkCore;

namespace Dota2WinPredictor.Data
{
    public class MyAppContext: DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {
        }

        public DbSet<HeroDTO> HeroDTOs { get; set; }
    }
}
