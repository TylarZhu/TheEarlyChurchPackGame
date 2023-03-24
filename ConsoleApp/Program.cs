using System;
using System.Text;
using System.Linq;
using ConsoleTables;

namespace ConsoleApp
{
    class Solution
    {
        private static int enterNumberOfPlayers()
        {
            string key;
            bool quit = false;
            int totalPlayers = 0;
            while (!quit)
            {
                Console.Write("Please enter number of players you have: ");
                key = Console.ReadLine()!;

                totalPlayers = GameProcess.tryPraseInput(key);

                if(totalPlayers < 8 || totalPlayers > 12)
                {
                    Console.WriteLine("Too Many or Not enough players! The number of players should be between 8 to 12.");
                } 
                else
                {
                    Console.WriteLine("Number of players: {0}. Is it corrent? 1 - Yes, 2 - No", totalPlayers);
                    ConsoleKeyInfo choice = Console.ReadKey(true);
                    Console.WriteLine();
                    while(choice.Key != ConsoleKey.D1 && choice.Key != ConsoleKey.D2)
                    {
                        Console.WriteLine("Please enter vailed choice!");
                        choice = Console.ReadKey(true);
                        Console.WriteLine();
                    }
                    if (choice.Key == ConsoleKey.D1)
                    {
                        quit = true;
                    }
                }
            }
            return totalPlayers;
        }
        private static int enterNumberOfMembers(int totalPlayers)
        {
            int members = 0;
            bool quit = false;
            while (!quit)
            {
                ConsoleKeyInfo choice;
                Console.Write("Please enter number of members in this party: ");
                string key = Console.ReadLine()!;

                members = GameProcess.tryPraseInput(key);

                if(members < 4 || totalPlayers - members < 4)
                {
                    Console.WriteLine("This party has less than 4 members or have too many members, so that another party have less than 4 members. The game is too unbalanced.");
                    continue;
                }
                if (members > totalPlayers / 2)
                {
                    Console.WriteLine("This party is more than half of total players! Are you sure about this?");
                    Console.WriteLine("1 - Yes, 2 - No");
                    choice = Console.ReadKey(true);
                    Console.WriteLine();
                    if (choice.Key == ConsoleKey.D1)
                    {
                        quit = true;
                    }
                }
                Console.WriteLine("Number of members in this party: {0}. Is it corrent? 1 - Yes, 2 - No", members);
                choice = Console.ReadKey(true);
                while(choice.Key != ConsoleKey.D1 && choice.Key != ConsoleKey.D2)
                {
                    Console.WriteLine("Number of members in this party: {0}. Is it corrent? 1 - Yes, 2 - No", members);
                    choice = Console.ReadKey(true);
                }
                if(choice.Key == ConsoleKey.D2)
                {
                    continue;
                }
                
                quit = true;
               
            }
            return members;
        }
        private static Identities[] issuedIdentityCards(int christians, int judaisms, int totalPlayers)
        {
            List<Identities> identities = new List<Identities>();
            Random rand = new Random();

            identities.Add(Identities.Judas);
            identities.Add(Identities.Scribes);
            identities.Add(Identities.Pharisee);
            identities.Add(Identities.Peter);
            identities.Add(Identities.John);
            identities.Add(Identities.Nicodemus);

            if(totalPlayers % 2 != 0) 
            {
                int addChoice = rand.Next(0,2);
                if(addChoice == 1)
                {
                    identities.Add(Identities.Judaism);
                    Console.WriteLine("An additional Jewish Congregation has been added!");
                }
                else
                {
                    identities.Add(Identities.Laity);
                    Console.WriteLine("An additional Laity has been added!");
                }
            }

            for(int i = 0; i < totalPlayers / 2 - 3; i ++)
            {
                identities.Add(Identities.Judaism);
            }
            for (int i = 0; i < totalPlayers / 2 - 3; i++)
            {
                identities.Add(Identities.Laity);
            }
            

            
            for(int i = identities.Count - 1; i > 0; i --)
            {
                int rad = rand.Next(0, i);
                Identities temp = identities[i];
                identities[i] = identities[rad];
                identities[rad] = temp;

            }

            /*for(int i = 0; i < identities.Count; i ++)
            {
                Console.WriteLine(identities[i]);
            }*/
            return identities.ToArray();
        }
        private static Players[] assignIdentitiesToPlayers(int totalPlayers, Identities[] identities)
        {
            List<Players> players = new List<Players>();
            for (int i = 0; i < totalPlayers; i++)
            {
                Console.WriteLine("Player {0} are you ready?", i + 1);
                ConsoleKeyInfo choice1 = new ConsoleKeyInfo();
                ConsoleKeyInfo choice2 = new ConsoleKeyInfo();
                string name = "";
                
                Console.Write("Please enter your name (no space): ");
                name = Console.ReadLine()!;
                while(name == "")
                {
                    Console.Write("Please enter your name (no space): ");
                    name = Console.ReadLine()!;
                }

                Console.WriteLine("if you are ready, press 1 to see your identity.");
                choice1 = Console.ReadKey(true);
                while(choice1.Key != ConsoleKey.D1)
                {
                    Console.WriteLine("if you are ready, press 1 to see your identity.");
                    choice1 = Console.ReadKey(true);
                }

                Console.Write("Your identity is: ");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(identities[i]);
                Console.ResetColor();
                Console.Write("!");
                Console.WriteLine();

                Console.WriteLine("Press 2 and pass on to the next player.");
                choice2 = Console.ReadKey(true);
                while (choice2.Key != ConsoleKey.D2)
                {
                    Console.WriteLine("If you ready, Press 2 and pass on to the next player.");
                    choice2 = Console.ReadKey(true);
                }
                Console.Clear();
                players.Add(new Players(identities[i], name, i + 1));
            }
            return players.ToArray();
        }
        private static void playBack(string[] playback)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Play Back:");
            Console.ResetColor();
            int day = 0;
            for (int i = 0; i < playback.Length; i++)
            {
                string[] y = playback[i].Split(' ');
                int playbackDay;
                if(int.TryParse(y[1], out playbackDay))
                {
                    if (day != playbackDay)
                    {
                        day = playbackDay;
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
                Console.WriteLine(playback[i]);
            }
        }
        private static void gameStart()
        {
       
            int totalPlayers = enterNumberOfPlayers();

            Console.WriteLine("Please enter number of Christians in the game!");
            int christians = enterNumberOfMembers(totalPlayers);
            int judaisms = totalPlayers - christians;


            Console.Clear();
            Console.WriteLine("You have in total of {0} players.", totalPlayers);
            Console.WriteLine("You have in total of {0} christians.", christians);
            Console.WriteLine("You have in total of {0} judaisms.", judaisms);

            Identities[] identities = issuedIdentityCards(christians, judaisms, totalPlayers);

            Console.WriteLine();
            Console.WriteLine("Game On!");
            Console.WriteLine("Please pass to the first player!");

            Players[] players = assignIdentitiesToPlayers(totalPlayers, identities);
            Dictionary<string, Players> playerDic = new Dictionary<string, Players>();

            /*double totalVotes = players.Sum(ob => ob.vote);*/
            double totalVotes = 0;
            int duplicateKey = 0;
            foreach (Players player in players)
            {
                if (playerDic.ContainsKey(player.identity.ToString()))
                {
                    duplicateKey++;
                    playerDic[player.identity.ToString() + duplicateKey.ToString()] = player;
                }
                else
                {
                    playerDic[player.identity.ToString()] = player;
                    totalVotes += player.vote;
                }  
            }
                
            List<string> playback = new List<string>(); // 复盘

            GameProcess.processing(playerDic, totalVotes, playback);

            GameProcess.press1ToContinue();
            playBack(playback.ToArray());

            
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Early Church Game!");

            gameStart();
        }
    }
}