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
		public DbSet<Verse> Verses { get; set; }
		public DbSet<Bridge> Bridges { get; set; }
		public DbSet<Chorus> Choruses { get; set; }
        public DbSet<LyricSegment> LyricSegments{ get; set; }

		public DbSet<UserFeedback> UserFeedback { get; set; }
		
    }
}
