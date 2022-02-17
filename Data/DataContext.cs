using Microsoft.EntityFrameworkCore;
using Wordle.Models;

namespace Wordle.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){} 
    }
}
