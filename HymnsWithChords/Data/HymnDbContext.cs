using HymnsWithChords.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HymnsWithChords.Data
{
	public class HymnDbContext : IdentityDbContext
	{
		public HymnDbContext(DbContextOptions<HymnDbContext> options)
			: base(options)
		{
		}
        public DbSet<Hymn> Hymns { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LyricSegment> LyricSegments{ get; set; }
		
    }
}
