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

		public DbSet<HymnBook> HymnBooks { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Hymn> Hymns { get; set; }        
		public DbSet<Verse> Verses { get; set; }
		public DbSet<Bridge> Bridges { get; set; }
		public DbSet<Chorus> Choruses { get; set; }
		public DbSet<LyricLine> LyricLines { get; set; }
		public DbSet<LyricSegment> LyricSegments{ get; set; }
		public DbSet<Chord> Chords { get; set; }
		public DbSet<ChordChart> ChordCharts { get; set; }
		public DbSet<UserFeedback> UserFeedback { get; set; }
		public DbSet<Page> Pages { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<ChordChart>()
				.HasOne(ch=>ch.Chord)
				.WithMany(cd=>cd.ChordCharts)
				.HasForeignKey(ch=>ch.ChordId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<LyricSegment>()
				.HasOne(seg=>seg.LyricLine)
				.WithMany(line=>line.LyricSegments)
				.HasForeignKey(seg=>seg.LiricLineId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<LyricSegment>()
				.HasOne(seg => seg.Chord)
				.WithMany(ch => ch.LyricSegments)
				.HasForeignKey(seg => seg.ChordId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<LyricLine>()
				.HasOne(seg => seg.Verse)
				.WithMany(br => br.LyricLines)
				.HasForeignKey(seg => seg.VerseId)
				.OnDelete(DeleteBehavior.Restrict);
			
			builder.Entity<LyricLine>()
				.HasOne(seg=>seg.Bridge)
				.WithMany(br=>br.LyricLines)
				.HasForeignKey(seg=>seg.BridgeId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<LyricLine>()
				.HasOne(seg=>seg.Chorus)
				.WithMany(ch=>ch.LyricLines)
				.HasForeignKey(seg=>seg.ChorusId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Verse>()
				.HasOne(ver=>ver.Hymn)
				.WithMany(hm=>hm.Verses)
				.HasForeignKey(ver=>ver.HymnId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Bridge>()
				.HasOne(br => br.Hymn)
				.WithMany(hm => hm.Bridges)
				.HasForeignKey(br => br.HymnId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Chorus>()
				.HasOne(ch => ch.Hymn)
				.WithMany(hm => hm.Choruses)
				.HasForeignKey(ch => ch.HymnId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<UserFeedback>()
				.HasOne(fb => fb.Hymn)
				.WithMany(hm => hm.Feedback)
				.HasForeignKey(fb => fb.HymnId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Hymn>()
				.HasOne(hm => hm.Category)
				.WithMany(cat => cat.Hymns)
				.HasForeignKey(hm => hm.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Category>()
				.HasOne(cat=>cat.HymnBook)
				.WithMany(hb=>hb.Categories)
				.HasForeignKey(cat=>cat.HymnBookId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<ChordChart>().ToTable("ChordCharts");

			builder.Entity<Chord>()
				.Property(c => c.Difficulty)
				.HasConversion<string>();
		}
	}
}
