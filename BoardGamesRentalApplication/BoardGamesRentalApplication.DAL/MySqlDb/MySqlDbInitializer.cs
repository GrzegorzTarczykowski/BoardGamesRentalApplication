using BoardGamesRentalApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BoardGamesRentalApplication.DAL.MySqlDb
{
    public class MySqlDbInitializer : DropCreateDatabaseAlways<MySqlDbContext>
    {
        protected override void Seed(MySqlDbContext context)
        {
            base.Seed(context);

            IList<BoardGameCategory> defaultBoardGameCategories = new List<BoardGameCategory>();

            defaultBoardGameCategories.Add(new BoardGameCategory() { Name = "Familijne" });
            defaultBoardGameCategories.Add(new BoardGameCategory() { Name = "Ekonomiczne" });
            defaultBoardGameCategories.Add(new BoardGameCategory() { Name = "Strategiczne" });
            defaultBoardGameCategories.Add(new BoardGameCategory() { Name = "Wojenne" });
            defaultBoardGameCategories.Add(new BoardGameCategory() { Name = "Kooperacyjne" });
            defaultBoardGameCategories.Add(new BoardGameCategory() { Name = "Dla dzieci" });
            defaultBoardGameCategories.Add(new BoardGameCategory() { Name = "Karciane" });
            defaultBoardGameCategories.Add(new BoardGameCategory() { Name = "Logiczne" });
            defaultBoardGameCategories.Add(new BoardGameCategory() { Name = "Edukacyjne" });
            defaultBoardGameCategories.Add(new BoardGameCategory() { Name = "Koœciane" });

            context.BoardGameCategories.AddRange(defaultBoardGameCategories);

            IList<BoardGameEvaluation> defaultBoardGameEvaluations = new List<BoardGameEvaluation>();

            defaultBoardGameEvaluations.Add(new BoardGameEvaluation() { Name = "Œwietna" });
            defaultBoardGameEvaluations.Add(new BoardGameEvaluation() { Name = "Bardzo dobra" });
            defaultBoardGameEvaluations.Add(new BoardGameEvaluation() { Name = "Dobra" });
            defaultBoardGameEvaluations.Add(new BoardGameEvaluation() { Name = "Przeciêtna" });
            defaultBoardGameEvaluations.Add(new BoardGameEvaluation() { Name = "S³aba" });
            defaultBoardGameEvaluations.Add(new BoardGameEvaluation() { Name = "Fatalna" });

            context.BoardGameEvaluations.AddRange(defaultBoardGameEvaluations);

            IList<BoardGamePublisher> defaultBoardGamePublishers = new List<BoardGamePublisher>();

            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Rebel" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Trefl" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Galakta" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Lacerta" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Hobbity" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Phalanx" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Gigamic" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Bard" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Nasza Ksiêgarnia" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Adamigo" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Axel" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Egmont" });

            context.BoardGamePublishers.AddRange(defaultBoardGamePublishers);

            IList<BoardGameState> defaultBoardGameStates = new List<BoardGameState>();

            defaultBoardGameStates.Add(new BoardGameState() { Name = "Bardzo dobry" });
            defaultBoardGameStates.Add(new BoardGameState() { Name = "Second Name" });
            defaultBoardGameStates.Add(new BoardGameState() { Name = "Uszkodzony" });

            context.BoardGameStates.AddRange(defaultBoardGameStates);

            IList<BoardGame> defaultBoardGames = new List<BoardGame>();

            defaultBoardGames.Add(new BoardGame() { Name = "Warcaby"
                                                    , Description = "Warcaby to klasyczna gra towarzyska znana na ca³ym œwiecie od niemal¿e oœmiuset lat. Celem gry jest „zbicie” wszystkich pionków przeciwnika."
                                                    , Content = "Plansza do gry 24 pionki w dwóch kolorach (czarnym i bia³ym)"
                                                    , MinimumAge = 5
                                                    , PlayerCount = 2
                                                    , BoardGamePublisherId = 10
                                                    , BoardGamePublisher = defaultBoardGamePublishers[9]
                                                    , BoardGameStateId = 2
                                                    , BoardGameState = defaultBoardGameStates[1]
                                                    , BoardGameCategoryId = 3
                                                    , BoardGameCategory = defaultBoardGameCategories[2]});
            defaultBoardGames.Add(new BoardGame() { Name = "Second Name"
                                                    , Description = "Second Description"
                                                    , Content = "Second Content"
                                                    , MinimumAge = 2
                                                    , PlayerCount = 2
                                                    , BoardGamePublisherId = 2
                                                    , BoardGamePublisher = defaultBoardGamePublishers[1]
                                                    , BoardGameStateId = 2
                                                    , BoardGameState = defaultBoardGameStates[1]
                                                    , BoardGameCategoryId = 3
                                                    , BoardGameCategory = defaultBoardGameCategories[2] });
            defaultBoardGames.Add(new BoardGame() { Name = "Third Name"
                                                    , Description = "Third Description"
                                                    , Content = "Third Content"
                                                    , MinimumAge = 3
                                                    , PlayerCount = 3
                                                    , BoardGamePublisherId = 3
                                                    , BoardGamePublisher = defaultBoardGamePublishers[2]
                                                    , BoardGameStateId = 3
                                                    , BoardGameState = defaultBoardGameStates[2]
                                                    , BoardGameCategoryId = 3
                                                    , BoardGameCategory = defaultBoardGameCategories[2] });
            defaultBoardGames.Add(new BoardGame() { Name = "Fourth Name"
                                                    , Description = "Fourth Description"
                                                    , Content = "Fourth Content"
                                                    , MinimumAge = 4
                                                    , PlayerCount = 4
                                                    , BoardGamePublisherId = 3
                                                    , BoardGamePublisher = defaultBoardGamePublishers[2]
                                                    , BoardGameStateId = 3
                                                    , BoardGameState = defaultBoardGameStates[2]
                                                    , BoardGameCategoryId = 3
                                                    , BoardGameCategory = defaultBoardGameCategories[2] });

            context.BoardGames.AddRange(defaultBoardGames);

            byte[] implicitPassword;
            string explicitPassword = "1234321PasswordHere";
            byte[] salt = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] password = Encoding.UTF8.GetBytes(explicitPassword);
                byte[] saltedPassword = password.Concat(salt).ToArray();
                implicitPassword = sha512.ComputeHash(saltedPassword);
            }

            User adminUser = new User
            {
                Email = "bielikba@wp.pl",
                Username = "ADMINISTRATOR",
                Salt = salt,
                Password = Convert.ToBase64String(implicitPassword),
                FirstName = "Ja",
                LastName = "Admin",
                UserType = UserType.Administrator,
                CreateDate = DateTime.Now,
                LastLogin = DateTime.MinValue
            };
            context.Users.Add(adminUser);
        }
    }
}
