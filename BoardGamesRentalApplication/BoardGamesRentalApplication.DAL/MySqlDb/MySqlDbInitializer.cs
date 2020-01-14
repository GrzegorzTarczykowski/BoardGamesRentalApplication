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
            defaultBoardGameStates.Add(new BoardGameState() { Name = "Dobry" });
            defaultBoardGameStates.Add(new BoardGameState() { Name = "Uszkodzony" });

            context.BoardGameStates.AddRange(defaultBoardGameStates);

            IList<BoardGame> defaultBoardGames = new List<BoardGame>();

            defaultBoardGames.Add(new BoardGame() { Name = "Warcaby"
                                                    , Description = "Warcaby to klasyczna gra towarzyska znana na ca³ym œwiecie od niemal¿e oœmiuset lat. Celem gry jest „zbicie” wszystkich pionków przeciwnika."
                                                    , Content = "Plansza do gry 24 pionki w dwóch kolorach (czarnym i bia³ym)"
                                                    , MinimumAge = 5
                                                    , GameTimeInMinutes = 15
                                                    , MinPlayerCount = 2
                                                    , MaxPlayerCount = 2
                                                    , RentalCostPerDay = 6.50M
                                                    , Quantity = 0
                                                    , BoardGamePublisherId = 10
                                                    , BoardGamePublisher = defaultBoardGamePublishers[9]
                                                    , BoardGameStateId = 2
                                                    , BoardGameState = defaultBoardGameStates[1]
                                                    , BoardGameCategoryId = 1
                                                    , BoardGameCategory = defaultBoardGameCategories[0]
                                                    , ImagePath = "/images/games/warcaby-list.png"
                                                    , DetailsImagePath = "/images/games/warcaby.png"});

            defaultBoardGames.Add(new BoardGame() { Name = "Rattus: Szczuro³ap"
                                                    , Description = @"Gra dziêki której nauka staje siê zabaw¹!
¯etony z literkami uk³adamy zakryte na stole,
planszê z piramid¹ uk³adamy przed graj¹cymi.Uczestnicy zabawy maj¹ za zadanie zbudowaæ na planszy piramidê z liter.Odkrywaj¹ kolejne literki i dok³adaj¹ na planszê,
musz¹ przy tym zachowaæ odpowiedni¹ kolejnoœæ liter.Gra pomaga w nauce poprawnego nazywania liter i w nauce kolejnoœci liter
w polskim alfabecie.
"
                                                    , Content = "32 ¿etony literek plansza 4 pionki instrukcja"
                                                    , MinimumAge = 5
                                                    , GameTimeInMinutes = 35
                                                    , MinPlayerCount = 2
                                                    , MaxPlayerCount = 4
                                                    , RentalCostPerDay = 3.50M
                                                    , Quantity = 3
                                                    , BoardGamePublisherId = 2
                                                    , BoardGamePublisher = defaultBoardGamePublishers[1]
                                                    , BoardGameStateId = 1
                                                    , BoardGameState = defaultBoardGameStates[0]
                                                    , BoardGameCategoryId = 2
                                                    , BoardGameCategory = defaultBoardGameCategories[1]
                                                    , ImagePath = "/images/games/rattus-list.png"
                                                    , DetailsImagePath = "/images/games/rattus.png" });

            defaultBoardGames.Add(new BoardGame() { Name = "Gemino"
                                                    , Description = "Gemino to bardzo przyjemna gra logiczna dla ca³ej rodziny. Proste zasady (podobne do kó³ko i krzy¿yk) umo¿liwiaj¹ ciekaw¹, wci¹gaj¹c¹ rozgrywkê. Dziêki temu, do gry chêtnie zasi¹d¹ zarówno rodzice, dzieci, jak i dziadkowie. Aby wygraæ, trzeba planowaæ swoje ruchy, blokowaæ i przechytrzyæ przeciwników na dwóch planszach jednoczeœnie!"
                                                    , Content = "2 dwustronne plansze 56 p³ytek instrukcja"
                                                    , MinimumAge = 7
                                                    , GameTimeInMinutes = 70
                                                    , MinPlayerCount = 2
                                                    , MaxPlayerCount = 4
                                                    , RentalCostPerDay = 2.50M
                                                    , Quantity = 2
                                                    , BoardGamePublisherId = 3
                                                    , BoardGamePublisher = defaultBoardGamePublishers[2]
                                                    , BoardGameStateId = 3
                                                    , BoardGameState = defaultBoardGameStates[2]
                                                    , BoardGameCategoryId = 3
                                                    , BoardGameCategory = defaultBoardGameCategories[2]
                                                    , ImagePath = "/images/games/gemino-list.png"
                                                    , DetailsImagePath = "/images/games/gemino.png"});

            defaultBoardGames.Add(new BoardGame() { Name = "Wyspa Skye"
                                                    , Description = "Miêkki piasek na pla¿ach, ³agodne wzgórza i potê¿ne ³añcuchy górskie. Tak, ten wspania³y krajobraz mo¿e oczarowaæ ka¿dego. Niech ten sielankowy obraz jednak ciê nie zmyli. Setki lat temu by³ to teren, na którym a¿ piêæ klanów walczy³o o dominacjê. Ka¿dy z nich zawziêcie walczy³ o przejêcie ka¿dego, nawet najmniejszego skrawka ziemi, jednoczeœnie próbuj¹c sprzedaæ tereny o ma³ej u¿ytecznoœci."
                                                    , Content = @"plansza do gry 16 ¿etonów punktacji 78 ¿etonów terenu 1 worek 5 znaczników punktacji 6 ¿etonów odrzucenia 6 parawanów 1 znacznik rund 1 ¿eton gracza rozpoczynaj¹cego 73 monety instrukcja"
                                                    , MinimumAge = 10
                                                    , GameTimeInMinutes = 35
                                                    , MinPlayerCount = 4
                                                    , MaxPlayerCount = 6
                                                    , RentalCostPerDay = 6.50M
                                                    , Quantity = 0
                                                    , BoardGamePublisherId = 3
                                                    , BoardGamePublisher = defaultBoardGamePublishers[2]
                                                    , BoardGameStateId = 3
                                                    , BoardGameState = defaultBoardGameStates[2]
                                                    , BoardGameCategoryId = 4
                                                    , BoardGameCategory = defaultBoardGameCategories[3]
                                                    , ImagePath = "/images/games/wyspaskye-list.png"
                                                    , DetailsImagePath = "/images/games/wyspaskye.png"});

            defaultBoardGames.Add(new BoardGame() { Name = "Res Arcana (edycja polska)"
                                                    , Description = @"W wysokiej wie¿y s³ynny alchemik przygotowuje magiczne eliksiry, zlewaj¹c ze sob¹ wielobarwne ciecze nie z tego œwiata, które przechowuje w ozdobnych flakonach. W œwiêtym gaju druid uciera w skupieniu zio³a potrzebne do pradawnego rytua³u, a skryty w ciemnoœciach katakumb nekromanta przywo³uje koœcianego smoka... Witamy w œwiecie Res Arcana! To tutaj ¿ycie, œmieræ, wigor, spokój i z³oto stanowi¹ esencje niezbêdne tym wszystkim, którzy paraj¹ siê sztuk¹ magii. "
                                                    , Content = "40 kart artefaktów 10 kart magów 10 kart monumentów 150 drewnianych znaczników esencji 5 p³ytek enklaw mocy(dwustronnych) 8 magicznych przedmiotów znacznik pierwszego gracza 12 ¿etonów esencji 5x 4 karty pomocy + p³ytka podsumowania instrukcja"
                                                    , MinimumAge = 15
                                                    , GameTimeInMinutes = 10
                                                    , MinPlayerCount = 2
                                                    , MaxPlayerCount = 5
                                                    , RentalCostPerDay = 9.50M
                                                    , Quantity = 1
                                                    , BoardGamePublisherId = 3
                                                    , BoardGamePublisher = defaultBoardGamePublishers[2]
                                                    , BoardGameStateId = 3
                                                    , BoardGameState = defaultBoardGameStates[2]
                                                    , BoardGameCategoryId = 5
                                                    , BoardGameCategory = defaultBoardGameCategories[4]
                                                    , ImagePath = "/images/games/resarcana-list.png"
                                                    , DetailsImagePath = "/images/games/resarcana.png"});

            defaultBoardGames.Add(new BoardGame() { Name = "Wszystko albo nic"
                                                    , Description = "Wszystko albo nic to gra kooperacyjna, czyli oparta na wspó³pracy. Macie wspólny cel, odniesiecie wiêc wspólne zwyciêstwo albo... wspólnie przegracie. Podczas gry bêdziecie wyk³adaæ na stó³ karty akcji o odpowiednich wartoœciach i kolorach. Celem ich wyk³adania jest spe³nienie warunków, opisanych na kartach zadañ. Wygracie, jeœli uda siê Wam wykonaæ wszystkie zadania! Zadania wydaj¹ siê proste, ale wcale nie bêdzie ³atwo je wykonaæ.Podczas gry obowi¹zuje zakaz komunikowania siê ze sob¹ na temat kolorów i wartoœci posiadanych przez Was kart, co znacznie utrudni wykonanie zadañ oraz dostarczy wielkich emocji!"
                                                    , Content = "56 kart akcji 50 kart zadañ instrukcja"
                                                    , MinimumAge = 18
                                                    , GameTimeInMinutes = 45
                                                    , MinPlayerCount = 2
                                                    , MaxPlayerCount = 4
                                                    , RentalCostPerDay = 3.50M
                                                    , Quantity = 3
                                                    , BoardGamePublisherId = 3
                                                    , BoardGamePublisher = defaultBoardGamePublishers[2]
                                                    , BoardGameStateId = 3
                                                    , BoardGameState = defaultBoardGameStates[2]
                                                    , BoardGameCategoryId = 6
                                                    , BoardGameCategory = defaultBoardGameCategories[5]
                                                    , ImagePath = "/images/games/wszystkoalbonic-list.png"
                                                    , DetailsImagePath = "/images/games/wszystkoalbonic.png"});

            defaultBoardGames.Add(new BoardGame() { Name = "Chiñczyk"
                                                    , Description = "Jest to popularna gra planszowa, przeznaczona dla dwóch, trzech lub czterech osób. To doskona³a gra towarzyska, idealna na niedzielne popo³udnia czy deszczowe dni. Uwielbiaj¹ j¹ zarówno dzieci jak i doroœli. Polega na tym, aby jak najszybciej przejœæ czterema pionkami przez wszystkie pola rozmieszczone dooko³a planszy. Ma byæ to przebycie drogi z pozycji pocz¹tkowej (tzw. „baza”) do pozycji koñcowej (tzw. „domek”) - szybciej ni¿ pozostali gracze. Ten zawodnik, który jako pierwszy umieœci swoje pionki w „domku”, ten wygrywa."
                                                    , Content = "plansza, 16 pionków (po 4 pionki w kolorze: ¿ó³tym, zielonym, niebieskim, czerwonym), kostka do gry"
                                                    , MinimumAge = 3
                                                    , GameTimeInMinutes = 30
                                                    , MinPlayerCount = 2
                                                    , MaxPlayerCount = 6
                                                    , RentalCostPerDay = 1.50M
                                                    , Quantity = 5
                                                    , BoardGamePublisherId = 2
                                                    , BoardGamePublisher = defaultBoardGamePublishers[1]
                                                    , BoardGameStateId = 1
                                                    , BoardGameState = defaultBoardGameStates[0]
                                                    , BoardGameCategoryId = 3
                                                    , BoardGameCategory = defaultBoardGameCategories[2]
                                                    , ImagePath = "/images/games/chinczyk.png"
                                                    , DetailsImagePath = "/images/games/chinczyk.png"});

            context.BoardGames.AddRange(defaultBoardGames);

            byte[] implicitPassword;
            string explicitPassword = "1234321PasswordHere";
            byte[] salt = new byte[32];
            byte[] implicitWorkerPassword;
            string workerPassword = "Pracownik123"; 
            byte[] salt2 = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                rng.GetBytes(salt2);
            }
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] password = Encoding.UTF8.GetBytes(explicitPassword);
                byte[] saltedPassword = password.Concat(salt).ToArray();
                implicitPassword = sha512.ComputeHash(saltedPassword);

                byte[] password2 = Encoding.UTF8.GetBytes(workerPassword);
                byte[] saltedPassword2 = password2.Concat(salt2).ToArray();
                implicitWorkerPassword = sha512.ComputeHash(saltedPassword2);
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

            User worker = new User
            {
                Email = "contact.me@wp.pl",
                Username = "Pracownik",
                Salt = salt2,
                Password = Convert.ToBase64String(implicitWorkerPassword),
                FirstName = "Jan",
                LastName = "Kowalski",
                UserType = UserType.Employee,
                CreateDate = DateTime.Now,
                LastLogin = DateTime.MinValue
            };
            context.Users.Add(worker);

            IList<ReservationStatus> defaultReservationStatuses = new List<ReservationStatus>();

            defaultReservationStatuses.Add(new ReservationStatus() { Name = "Rozpoczêta" });
            defaultReservationStatuses.Add(new ReservationStatus() { Name = "Zakoñczona" });
            defaultReservationStatuses.Add(new ReservationStatus() { Name = "Przetrzymanie" });

            context.ReservationStatuses.AddRange(defaultReservationStatuses);

            IList<DiscountCodeStatus> discountCodeStatuses = new List<DiscountCodeStatus>();

            discountCodeStatuses.Add(new DiscountCodeStatus { Name = "Niedostêpny" });
            discountCodeStatuses.Add(new DiscountCodeStatus { Name = "Dostêpny" });
            discountCodeStatuses.Add(new DiscountCodeStatus { Name = "U¿yty" });

            context.DiscountCodeStatuses.AddRange(discountCodeStatuses);

            IList<BoardGameNote> boardGameNotes = new List<BoardGameNote>();

            boardGameNotes.Add(new BoardGameNote { Author = "Zbigniew", Content = "Œwietna gra polecam! Gra³em w ni¹ ca³¹ noc", BoardGameId = 1, BoardGame = defaultBoardGames[0] });
            boardGameNotes.Add(new BoardGameNote { Author = "Ula", Content = "Kocham t¹ gre! Jest rewelacyjna. Nie moge siê doczekaæ kolejnej parti", BoardGameId = 2, BoardGame = defaultBoardGames[1] });
            boardGameNotes.Add(new BoardGameNote { Author = "Cichy gracz", Content = "Najlepsza gra na œwiecie", BoardGameId = 1, BoardGame = defaultBoardGames[0] });

            context.BoardGameNotes.AddRange(boardGameNotes);
        }
    }
}
