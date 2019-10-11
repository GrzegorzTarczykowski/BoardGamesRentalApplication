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

            IList<BoardGamePublisher> defaultBoardGamePublishers = new List<BoardGamePublisher>();

            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Rebel" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Trefl" });
            defaultBoardGamePublishers.Add(new BoardGamePublisher() { Name = "Galakta" });

            context.BoardGamePublishers.AddRange(defaultBoardGamePublishers);

            IList<BoardGameState> defaultBoardGameStates = new List<BoardGameState>();

            defaultBoardGameStates.Add(new BoardGameState() { Name = "First Name" });
            defaultBoardGameStates.Add(new BoardGameState() { Name = "Second Name" });
            defaultBoardGameStates.Add(new BoardGameState() { Name = "Third Name" });

            context.BoardGameStates.AddRange(defaultBoardGameStates);

            IList<BoardGame> defaultBoardGames = new List<BoardGame>();

            defaultBoardGames.Add(new BoardGame() { Name = "First Name"
                                                    , Description = "First Description"
                                                    , Content = "First Content"
                                                    , MinimumAge = 1
                                                    , PlayerCount = 1
                                                    , BoardGamePublisherId = 1
                                                    , BoardGamePublisher = defaultBoardGamePublishers[0]
                                                    , BoardGameStateId = 1
                                                    , BoardGameState = defaultBoardGameStates[0] });
            defaultBoardGames.Add(new BoardGame() { Name = "Second Name"
                                                    , Description = "Second Description"
                                                    , Content = "Second Content"
                                                    , MinimumAge = 2
                                                    , PlayerCount = 2
                                                    , BoardGamePublisherId = 2
                                                    , BoardGamePublisher = defaultBoardGamePublishers[1]
                                                    , BoardGameStateId = 2
                                                    , BoardGameState = defaultBoardGameStates[1] });
            defaultBoardGames.Add(new BoardGame() { Name = "Third Name"
                                                    , Description = "Third Description"
                                                    , Content = "Third Content"
                                                    , MinimumAge = 3
                                                    , PlayerCount = 3
                                                    , BoardGamePublisherId = 3
                                                    , BoardGamePublisher = defaultBoardGamePublishers[2]
                                                    , BoardGameStateId = 3
                                                    , BoardGameState = defaultBoardGameStates[2] });
            defaultBoardGames.Add(new BoardGame() { Name = "Fourth Name"
                                                    , Description = "Fourth Description"
                                                    , Content = "Fourth Content"
                                                    , MinimumAge = 4
                                                    , PlayerCount = 4
                                                    , BoardGamePublisherId = 3
                                                    , BoardGamePublisher = defaultBoardGamePublishers[2]
                                                    , BoardGameStateId = 3
                                                    , BoardGameState = defaultBoardGameStates[2] });

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
