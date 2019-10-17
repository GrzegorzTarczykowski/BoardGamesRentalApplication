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
        public DbSet<BoardGameCategory> BoardGameCategories { get; set; }
        public DbSet<BoardGameEvaluation> BoardGameEvaluations { get; set; }
        public DbSet<BoardGameNote> BoardGameNotes { get; set; }
        public DbSet<BoardGamePublisher> BoardGamePublishers { get; set; }
        public DbSet<BoardGameState> BoardGameStates { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<BoardGame>()
                .HasMany<BoardGameEvaluation>(bg => bg.BoardGameEvaluations)
                .WithMany(bge => bge.BoardGames)
                .Map(bgbge =>
                {
                    bgbge.MapLeftKey($"{nameof(BoardGame)}Id");
                    bgbge.MapRightKey($"{nameof(BoardGameEvaluation)}Id");
                    bgbge.ToTable($"{nameof(BoardGame)}{nameof(BoardGameEvaluation)}");
                });

            //modelBuilder.Entity<BoardGamePublisher>()
            //    .HasMany<BoardGame>(bgp => bgp.BoardGames)
            //    .WithRequired(bg => bg.BoardGamePublisher)
            //    .WillCascadeOnDelete(false);//TODO: Decide if should cascade delete or not
            
            modelBuilder.Entity<BoardGame>()
                        .HasRequired(bg => bg.BoardGamePublisher)
                        .WithMany(bgp => bgp.BoardGames)
                        .HasForeignKey(bg => bg.BoardGamePublisherId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<BoardGame>()
                        .HasRequired(bg => bg.BoardGameState)
                        .WithMany(bgs => bgs.BoardGames)
                        .HasForeignKey(bg => bg.BoardGameStateId)
                        .WillCascadeOnDelete(false);
        }
    }
}
