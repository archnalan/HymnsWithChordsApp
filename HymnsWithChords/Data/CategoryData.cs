using HymnsWithChords.Data;
using Microsoft.EntityFrameworkCore;

namespace HymnsWithChords.Models
{
	public class CategoryData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using(var context = new HymnDbContext(serviceProvider
				.GetRequiredService<DbContextOptions<HymnDbContext>>()))
			{
				if (context.Categories.Any())
				{
					return;
				}
				context.Categories.AddRange(
				new Category { Id = 1, Name = "WORSHIP", ParentCategoryId = null },
				new Category { Id = 2, Name = "Adoration and Praise", ParentCategoryId = 1 },
				new Category { Id = 3, Name = "Morning Worship", ParentCategoryId = 1 },
				new Category { Id = 4, Name = "Evening Worship", ParentCategoryId = 1 },
				new Category { Id = 5, Name = "Opening of Worship", ParentCategoryId = 1 },
				new Category { Id = 6, Name = "Close of Worship", ParentCategoryId = 1 },

				new Category { Id = 7, Name = "TRINITY", ParentCategoryId = null },

				new Category { Id = 8, Name = "GOD THE FATHER", ParentCategoryId = null },
				new Category { Id = 9, Name = "Love of God", ParentCategoryId = 8 },
				new Category { Id = 10, Name = "Majesty and Power of God", ParentCategoryId = 8 },
				new Category { Id = 11, Name = "Power of God in Nature", ParentCategoryId = 8 },
				new Category { Id = 12, Name = "Faithfulness of God", ParentCategoryId = 8 },
				new Category { Id = 13, Name = "Grace and Mercy of God", ParentCategoryId = 8 },

				new Category { Id = 14, Name = "JESUS CHRIST", ParentCategoryId = null },
				new Category { Id = 15, Name = "FirstAdvent", ParentCategoryId = 14 },
				new Category { Id = 16, Name = "Birth", ParentCategoryId = 14 },
				new Category { Id = 17, Name = "Life and Ministry", ParentCategoryId = 14 },
				new Category { Id = 18, Name = "Sufferings and Death", ParentCategoryId = 14 },
				new Category { Id = 19, Name = "Ressurection and Ascension", ParentCategoryId = 14 },
				new Category { Id = 20, Name = "Priesthood", ParentCategoryId = 14 },
				new Category { Id = 21, Name = "Love Of Christ for Us", ParentCategoryId = 14 },
				new Category { Id = 22, Name = "SecondAdvent", ParentCategoryId = 14 },
				new Category { Id = 23, Name = "Kingdom and Reign", ParentCategoryId = 14 },
				new Category { Id = 24, Name = "Glory and Praise", ParentCategoryId = 14 },

				new Category { Id = 25, Name = "HOLY SPIRIT", ParentCategoryId = null },

				new Category { Id = 26, Name = "GOSPEL", ParentCategoryId = null },
				new Category { Id = 27, Name = "Invitation", ParentCategoryId = 26 },
				new Category { Id = 28, Name = "Repentance", ParentCategoryId = 26 },
				new Category { Id = 29, Name = "Forgiveness", ParentCategoryId = 26 },
				new Category { Id = 30, Name = "Consecration", ParentCategoryId = 26 },
				new Category { Id = 31, Name = "Baptism", ParentCategoryId = 26 },
				new Category { Id = 32, Name = "Salvation", ParentCategoryId = 26 },
				new Category { Id = 33, Name = "Redemption", ParentCategoryId = 26 },

				new Category { Id = 34, Name = "CHRISTIAN CHURCH", ParentCategoryId = null },
				new Category { Id = 35, Name = "Community in Christ", ParentCategoryId = 34 },
				new Category { Id = 36, Name = "Mission of the Church", ParentCategoryId = 34 },
				new Category { Id = 37, Name = "Church Dedication", ParentCategoryId = 34 },
				new Category { Id = 38, Name = "Ordination", ParentCategoryId = 34 },
				new Category { Id = 39, Name = "Child Dedication", ParentCategoryId = 34 },

				new Category { Id = 40, Name = "DOCTIRINES", ParentCategoryId = null },
				new Category { Id = 41, Name = "Sabbath", ParentCategoryId = 40 },
				new Category { Id = 42, Name = "Communion", ParentCategoryId = 40 },
				new Category { Id = 43, Name = "Law and Grace", ParentCategoryId = 40 },
				new Category { Id = 44, Name = "Spiritual Gifts", ParentCategoryId = 40 },
				new Category { Id = 45, Name = "Judgement", ParentCategoryId = 40 },
				new Category { Id = 46, Name = "Resurrection of the Saints", ParentCategoryId = 40 },
				new Category { Id = 47, Name = "Eternal Life", ParentCategoryId = 40 },

				new Category { Id = 48, Name = "EARLY ADVENT", ParentCategoryId = null },

				new Category { Id = 49, Name = "CHRISTIAN LIFE", ParentCategoryId = null },
				new Category { Id = 50, Name = "Our Love for God", ParentCategoryId = 49 },
				new Category { Id = 51, Name = "Joy and Peace", ParentCategoryId = 49 },
				new Category { Id = 52, Name = "Hope and Comfort", ParentCategoryId = 49 },
				new Category { Id = 53, Name = "Meditation and Prayer", ParentCategoryId = 49 },
				new Category { Id = 54, Name = "Faith and Trust", ParentCategoryId = 49 },
				new Category { Id = 55, Name = "Guidance", ParentCategoryId = 49 },
				new Category { Id = 56, Name = "Thankfulness", ParentCategoryId = 49 },
				new Category { Id = 57, Name = "Humility", ParentCategoryId = 49 },
				new Category { Id = 58, Name = "Loving Service", ParentCategoryId = 49 },
				new Category { Id = 59, Name = "Love for One Another", ParentCategoryId = 49 },
				new Category { Id = 60, Name = "Obedience", ParentCategoryId = 49 },
				new Category { Id = 61, Name = "Watchfulness", ParentCategoryId = 49 },
				new Category { Id = 62, Name = "Christian Warfare", ParentCategoryId = 49 },
				new Category { Id = 63, Name = "Pilgrimage", ParentCategoryId = 49 },
				new Category { Id = 64, Name = "Stewardship", ParentCategoryId = 49 },
				new Category { Id = 65, Name = "Health and Wholeness", ParentCategoryId = 49 },
				new Category { Id = 66, Name = "Love of Country", ParentCategoryId = 49 },

				new Category { Id = 67, Name = "CHRISTIAN HOME", ParentCategoryId = null },
				new Category { Id = 68, Name = "Love in the Home", ParentCategoryId = 67 },
				new Category { Id = 69, Name = "Marriage", ParentCategoryId = 67 },

				new Category { Id = 70, Name = "SENTENCES AND RESPONSES", ParentCategoryId = null }
				);
			}

		}
		
	}
}
