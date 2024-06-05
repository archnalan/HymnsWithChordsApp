using HymnsWithChords.Data;
using Microsoft.EntityFrameworkCore;

namespace HymnsWithChords.Models
{
	public class CategoryData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new HymnDbContext(serviceProvider
				.GetRequiredService<DbContextOptions<HymnDbContext>>()))
			{
				if (context.Categories.Any())
				{
					Console.WriteLine("Categories Already seeded");
					return; // DB already been seeded
				}
				Console.WriteLine("Seeding categories...");
				context.Categories.AddRange(
					new Category { Name = "WORSHIP", ParentCategoryId = null },
					new Category { Name = "Adoration and Praise", ParentCategoryId = 1 },
					new Category { Name = "Morning Worship", ParentCategoryId = 1 },
					new Category { Name = "Evening Worship", ParentCategoryId = 1 },
					new Category { Name = "Opening of Worship", ParentCategoryId = 1 },
					new Category { Name = "Close of Worship", ParentCategoryId = 1 },

					new Category { Name = "TRINITY", ParentCategoryId = null },

					new Category { Name = "GOD THE FATHER", ParentCategoryId = null },
					new Category { Name = "Love of God", ParentCategoryId = 8 },
					new Category { Name = "Majesty and Power of God", ParentCategoryId = 8 },
					new Category { Name = "Power of God in Nature", ParentCategoryId = 8 },
					new Category { Name = "Faithfulness of God", ParentCategoryId = 8 },
					new Category { Name = "Grace and Mercy of God", ParentCategoryId = 8 },

					new Category { Name = "JESUS CHRIST", ParentCategoryId = null },
					new Category { Name = "First Advent", ParentCategoryId = 14 },
					new Category { Name = "Birth", ParentCategoryId = 14 },
					new Category { Name = "Life and Ministry", ParentCategoryId = 14 },
					new Category { Name = "Sufferings and Death", ParentCategoryId = 14 },
					new Category { Name = "Resurrection and Ascension", ParentCategoryId = 14 },
					new Category { Name = "Priesthood", ParentCategoryId = 14 },
					new Category { Name = "Love Of Christ for Us", ParentCategoryId = 14 },
					new Category { Name = "Second Advent", ParentCategoryId = 14 },
					new Category { Name = "Kingdom and Reign", ParentCategoryId = 14 },
					new Category { Name = "Glory and Praise", ParentCategoryId = 14 },

					new Category { Name = "HOLY SPIRIT", ParentCategoryId = null },

					new Category { Name = "GOSPEL", ParentCategoryId = null },

					new Category { Name = "Invitation", ParentCategoryId = 26 },
					new Category { Name = "Repentance", ParentCategoryId = 26 },
					new Category { Name = "Forgiveness", ParentCategoryId = 26 },
					new Category { Name = "Consecration", ParentCategoryId = 26 },
					new Category { Name = "Baptism", ParentCategoryId = 26 },
					new Category { Name = "Salvation", ParentCategoryId = 26 },
					new Category { Name = "Redemption", ParentCategoryId = 26 },

					new Category { Name = "CHRISTIAN CHURCH", ParentCategoryId = null },
					new Category { Name = "Community in Christ", ParentCategoryId = 34 },
					new Category { Name = "Mission of the Church", ParentCategoryId = 34 },
					new Category { Name = "Church Dedication", ParentCategoryId = 34 },
					new Category { Name = "Ordination", ParentCategoryId = 34 },
					new Category { Name = "Child Dedication", ParentCategoryId = 34 },

					new Category { Name = "DOCTRINES", ParentCategoryId = null },
					new Category { Name = "Sabbath", ParentCategoryId = 40 },
					new Category { Name = "Communion", ParentCategoryId = 40 },
					new Category { Name = "Law and Grace", ParentCategoryId = 40 },
					new Category { Name = "Spiritual Gifts", ParentCategoryId = 40 },
					new Category { Name = "Judgement", ParentCategoryId = 40 },
					new Category { Name = "Resurrection of the Saints", ParentCategoryId = 40 },
					new Category { Name = "Eternal Life", ParentCategoryId = 40 },

					new Category { Name = "EARLY ADVENT", ParentCategoryId = null },

					new Category { Name = "CHRISTIAN LIFE", ParentCategoryId = null },
					new Category { Name = "Our Love for God", ParentCategoryId = 49 },
					new Category { Name = "Joy and Peace", ParentCategoryId = 49 },
					new Category { Name = "Hope and Comfort", ParentCategoryId = 49 },
					new Category { Name = "Meditation and Prayer", ParentCategoryId = 49 },
					new Category { Name = "Faith and Trust", ParentCategoryId = 49 },
					new Category { Name = "Guidance", ParentCategoryId = 49 },
					new Category { Name = "Thankfulness", ParentCategoryId = 49 },
					new Category { Name = "Humility", ParentCategoryId = 49 },
					new Category { Name = "Loving Service", ParentCategoryId = 49 },
					new Category { Name = "Love for One Another", ParentCategoryId = 49 },
					new Category { Name = "Obedience", ParentCategoryId = 49 },
					new Category { Name = "Watchfulness", ParentCategoryId = 49 },
					new Category { Name = "Christian Warfare", ParentCategoryId = 49 },
					new Category { Name = "Pilgrimage", ParentCategoryId = 49 },
					new Category { Name = "Stewardship", ParentCategoryId = 49 },
					new Category { Name = "Health and Wholeness", ParentCategoryId = 49 },
					new Category { Name = "Love of Country", ParentCategoryId = 49 },

					new Category { Name = "CHRISTIAN HOME", ParentCategoryId = null },
					new Category { Name = "Love in the Home", ParentCategoryId = 67 },
					new Category { Name = "Marriage", ParentCategoryId = 67 },

					new Category { Name = "SENTENCES AND RESPONSES", ParentCategoryId = null }
				);
				try
				{					
					context.SaveChanges(); // Ensures changes are saved to the database					
					Console.WriteLine("Categories seeded successfully!");
				}
				catch(Exception ex)
				{
					Console.WriteLine($"Error Seeding Data: { ex.Message}");
					throw;
				}				
			}
		}
	}


}

