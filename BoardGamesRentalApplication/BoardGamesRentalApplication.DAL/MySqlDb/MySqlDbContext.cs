using BoardGamesRentalApplication.DAL.Models;
using MySql.Data.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BoardGamesRentalApplication.DAL.MySqlDb
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext() : base("BoardGamesRentalConnectionStrings")
        {
        }

        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<BoardGameEvaluation> BoardGameEvaluations { get; set; }
        public DbSet<BoardGameState> BoardGameStates { get; set; }
        public DbSet<BoardGameCategory> BoardGameCategories { get; set; }
        public DbSet<BoardGamePublisher> BoardGamePublishers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<BoardGame>()
                .HasMany<BoardGameEvaluation>(bg => bg.BoardGameEvaluations)
                .WithMany(bge => bge.BoardGames)
                .Map(bgbge =>
                {
                    bgbge.MapLeftKey("BoardGameId");
                    bgbge.MapRightKey("BoardGameEvaluationId");
                    bgbge.ToTable("BoardGameBoardGameEvaluation");
                });
            modelBuilder.Entity<BoardGame>()
                .HasMany<BoardGameCategory>(bg => bg.BoardGameCategories)
                .WithOptional()
                .Map(bgbgc =>
                {
                    bgbgc.MapKey("BoardGameCategoryId");
                });
            modelBuilder.Entity<BoardGamePublisher>()
                .HasMany<BoardGame>(bgp => bgp.BoardGames)
                .WithRequired(bg => bg.Publisher)
                .Map(bgbgp =>
                {
                    bgbgp.MapKey("BoardGamePublisherId");
                })
                .WillCascadeOnDelete(false);//TODO: Decide if should cascade delete or not
        }
    }
}
