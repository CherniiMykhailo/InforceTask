using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InforceTask.Models
{
    public class UrlDbContex : DbContext
    {
        public UrlDbContex(DbContextOptions<UrlDbContex> options)
            : base(options) { }

        public DbSet<Url> Urls { get; set; }
    }
}
