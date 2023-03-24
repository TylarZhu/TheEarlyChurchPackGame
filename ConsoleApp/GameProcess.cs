using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace ConsoleApp
{
    internal class GameProcess
    {
        // Global Variables
        private static double judaismLostVote = 0;
        private static double christianLostVote = 0;
        private static double judaismTotalVote = 0;

        // Data in playerDic is use to display to the users, it is not used in the program! (e.g. player's number - 1)
        private static void printPlayersInfo(Players[] players)
        {
            Console.WriteLine("Players Information:");
            var table = new ConsoleTable("Number", "Name", "Identity", "Vote", "Priest", "Ruler Of The Synagogue", "In Game");
            for (int i = 0; i < players.Length; i++)
            {
                List<string> row = new List<string>();
                if (players[i].identity == Identities.Judaism ||
                    players[i].identity == Identities.Judas ||
                    players[i].identity == Identities.Pharisee ||
                    players[i].identity == Identities.Scribes)
                {
                    string voteText = players[i].number.ToString() + " **";
                    row.Add(voteText);
                }
                else
                {
                    row.Add(players[i].number.ToString());
                }
                row.Add(players[i].name);
                row.Add(players[i].identity.ToString());
                row.Add(players[i].vote.ToString());

                if (players[i].priest)
                {
                    row.Add("True");
                }
                else
                {
                    row.Add("");
                }

                if (players[i].rulerOfTheSynagogue)
                {
                    row.Add("True");
                }
                else
                {
                    row.Add("");
                }

                if (players[i].inGame)
                {
                    row.Add("");
                }
                else
                {
                    row.Add("Out");
                }
                table.AddRow(row.ToArray());
            }
            table.Write(Format.Alternative);
        }
        /// <summary>
        /// Return true if the win condition does not meet. Return false if the win condition meets.
        /// </summary>
        /// <param name="judaismTotalVote"></param>
        /// <param name="judaismLostVote"></param>
        /// <param name="christianLostVote"></param>
        /// <param name="totalVotes"></param>
        /// <returns></returns>
        private static bool winCondition(double totalVotes)
        {
            
            Console.WriteLine("Judaism lose percentage ( > 0.5 Christian Wins): " + judaismLostVote / judaismTotalVote);
            Console.WriteLine("Christian lose percentage ( > 0.35 Judaism Wins): " + christianLostVote / totalVotes);

            

            if (judaismLostVote / judaismTotalVote > 0.5)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Christian Wins!");
                Console.ResetColor();
                return false;
            }
            else if (christianLostVote / totalVotes > 0.35)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Judaism Wins!");
                Console.ResetColor();
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void press1ToContinue()
        {
            Console.WriteLine("Press 1 to continue!");
            ConsoleKeyInfo choice1 = Console.ReadKey(true);
            while (choice1.Key != ConsoleKey.D1)
            {
                Console.WriteLine("Press 1 to continue!");
                choice1 = Console.ReadKey(true);
            }
        }
        private static void randomGenerateATopic(Dictionary<string, Players> playerDic, List<string> playback, int day)
        {
            Topics[] topics = Enum.GetValues(typeof(Topics)).Cast<Topics>().ToArray();
            Random rand = new Random();
            bool spiritualQuestion = false;

            Console.Write("Today's topic is: ");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            switch (topics[rand.Next(topics.Length)])
            {
                case Topics.Topic1:
                    Console.Write("Who is the Scribes?");
                    playback.Add($"Day {day} daylight, the discussion topic is Who is the Scribes?");
                    break;
                case Topics.Topic2:
                    Console.Write("Who is the Pharisees?");
                    playback.Add($"Day {day} daylight, the discussion topic is Who is the Pharisees?");
                    break;
                case Topics.Topic3:
                    Console.Write("Who is Judas?");
                    playback.Add($"Day {day} daylight, the discussion topic is Who is the Judas?");
                    break;
                case Topics.Topic4:
                    Console.Write("Who is Peter?");
                    playback.Add($"Day {day} daylight, the discussion topic is Who is the Peter?");
                    break;
                case Topics.Topic5:
                    Console.Write("Who is John?");
                    playback.Add($"Day {day} daylight, the discussion topic is Who is the John?");
                    break;
                case Topics.Topic6:
                    Console.Write("Who is Nicodemus?");
                    playback.Add($"Day {day} daylight, the discussion topic is Who is the Nicodemus?");
                    break;
                case Topics.Topic7:
                    Console.Write("Spiritual Depth Question1.");
                    spiritualQuestion = true;
                    break;
                case Topics.Topic8:
                    Console.Write("Spiritual Depth Question2.");
                    spiritualQuestion = true;
                    break;
                case Topics.Topic9:
                    Console.Write("Spiritual Depth Question3.");
                    spiritualQuestion = true;
                    break;
            }
            Console.ResetColor();
            Console.WriteLine();

            if (spiritualQuestion)
            {
                foreach (KeyValuePair<string, Players> player in playerDic)
                {
                    Console.WriteLine("Do you want to choose a hard question or easy question? Hard -- 1 Easy -- 2");
                    ConsoleKeyInfo choice1 = Console.ReadKey(true);
                    while (choice1.Key != ConsoleKey.D1 && choice1.Key != ConsoleKey.D2)
                    {
                        Console.WriteLine("Do you want to choose a hard question or easy question? Hard -- 1 Easy -- 2");
                        choice1 = Console.ReadKey(true);
                    }

                    ConsoleKeyInfo choice2 = Console.ReadKey(true);
                    Console.WriteLine("Did he answered correctly? Yes -- 1 No -- 2");
                    while (choice2.Key != ConsoleKey.D1 && choice2.Key != ConsoleKey.D2)
                    {
                        Console.WriteLine("Did he answered correctly? Yes -- 1 No -- 2");
                        choice2 = Console.ReadKey(true);
                    }
                    if (choice1.Key == ConsoleKey.D1 && choice2.Key == ConsoleKey.D1)
                    {
                        player.Value.setVote(player.Value.vote + 0.5);
                        playback.Add($"Day {day} daylight, {player.Value.number} {player.Value.name} correctly answered the hard Spiritual Depth Question! His/her vote weight increase 0.5!");
                    }
                    else if(choice1.Key == ConsoleKey.D2 && choice2.Key == ConsoleKey.D1)
                    {
                        player.Value.setVote(player.Value.vote + 0.25);
                        playback.Add($"Day {day} daylight, {player.Value.number} {player.Value.name} correctly answered the easy Spiritual Depth Question! His/her vote weight increase 0.25!");
                    }
                    else
                    {
                        playback.Add($"Day {day} daylight, {player.Value.number} {player.Value.name} did not correctly answered the hard Spiritual Depth Question! His/her vote weight does not change!");
                    }
                }
            }
        }
        /// <summary>
        /// return most vote player number.
        /// </summary>
        /// <param name="playerDic"></param>
        /// <param name="playback"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        private static int makingVoteRound(Dictionary<string, Players> playerDic, List<string> playback, int day)
        {
            Players[] players = playerDic.Values.ToArray();
            Dictionary<int, double> voteList = new Dictionary<int, double>(); // player's number that been voted -> number of votes
            int votePeople = -1;
/*            int equalVotesDiscuss = 0;*/

            /*while (true)
            {*/
            for (int i = 0; i < players.Length; i++)
            {
                if (!players[i].inGame)
                {
                    continue;
                }
                Console.Clear();
                Console.WriteLine("Player {0} make your vote!", i + 1);
                simplePrintPlayerInfo(players);
                // 玩家输入想投谁(player's number)
                while (true)
                {
                    Console.Write("Please decide which person you want to vote (enter a number): ");
                    bool quit = false;

                    while (!quit)
                    {
                        string key = Console.ReadLine()!;
                        votePeople = tryPraseInput(key);

                        if (votePeople < 1 ||
                            votePeople > playerDic.Count ||
                            playerDic.ElementAt(votePeople - 1).Value.inGame == false)
                        {
                            Console.WriteLine("Wrong number! Please enter a vaild value (1 ~ {0}) and you cannot vote a person who has already been exiled!", playerDic.Count);
                            Console.Write("Please enter again: ");
                        }
                        else
                        {
                            quit = true;
                        }
                    }
                    Console.WriteLine("You vote {0} {1} to zero his/her vote weight, is that correct? Yes -- 1 No -- 2", players[votePeople - 1].number, players[votePeople - 1].name);
                    ConsoleKeyInfo consoleKey = Console.ReadKey(true);
                    while (consoleKey.Key != ConsoleKey.D1 && consoleKey.Key != ConsoleKey.D2)
                    {
                        Console.WriteLine("You vote {0} {1} to zero his/her vote weight, is that correct? Yes -- 1 No -- 2", players[votePeople - 1].number, players[votePeople - 1].name);
                        consoleKey = Console.ReadKey(true);
                    }
                    if (consoleKey.Key == ConsoleKey.D1)
                    {
                        break;
                    }
                }

                // 被投的玩家加票数
                if (!voteList.ContainsKey(votePeople - 1))
                {
                    voteList[votePeople - 1] = players[i].vote;
                }
                else
                {
                    voteList[votePeople - 1] += players[i].vote;
                }
            }
            double maxVote = voteList.Values.Max();
            List<Players> equalVotePlayers = new List<Players>();
            foreach (var gamer in voteList)
            {
                if (gamer.Value == maxVote)
                {
                    equalVotePlayers.Add(players[gamer.Key]);
                }
            }

            // 有平票的人
            if (equalVotePlayers.Count > 1)
            {
                // 如何连着两次平票，则没有人去权
                /* if (equalVotesDiscuss < 1)
                {
                    Console.Clear();
                    for (int i = 0; i < equalVotePlayers.Count; i++)
                    {
                        Console.Write(equalVotePlayers[i].number + " " + equalVotePlayers[i].name + ", ");
                    }
                    Console.WriteLine("have equal amount of votes!");
                    Console.WriteLine("Please discuss one more time and decide who is going to zero their vote weight!");
                    equalVotesDiscuss++;
                    press1ToContinue();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("have equal amount of votes again!");
                    Console.WriteLine("This round, no one has lost vote weight!");
                    return -1;
                }*/
                string equalVotePlayback = "";
                for (int i = 0; i < equalVotePlayers.Count; i++)
                {
                    equalVotePlayback += equalVotePlayers[i].number + " " + equalVotePlayers[i].name + ", ";
                    
                }
                Console.WriteLine(equalVotePlayback + " have same amount of vote!");
                playback.Add($"Day {day} daylight vote: " + equalVotePlayback + " have same amount of vote! No one has lost vote weight!");
                Console.Clear();
/*                Console.WriteLine("There are two players have equal amount of votes!");*/
                Console.WriteLine("This round, no one has lost vote weight!");
                return -1;

            }
            else
            {
                return equalVotePlayers[0].number - 1;
            }
            /*}*/
        }
        private static void daylight(int day, Dictionary<string, Players> playerDic, string lastExiled, List<string> playback)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Day {0}.", day);
            Console.ResetColor();
            Console.WriteLine();
            printPlayersInfo(playerDic.Values.ToArray());

            // 讨论话题
            if (day > 1)
            {
                if (lastExiled == "")
                {
                    playback.Add($"Day {day} daylight, No one is been exiled last night!");
                    Console.WriteLine("Last night, No one is been exiled last night!");
                }
                else
                {
                    Console.Write("Last night, ");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("{0} {1}", playerDic[lastExiled].number, playerDic[lastExiled].name);
                    Console.ResetColor();
                    Console.WriteLine(" has been exiled!");
                    playback.Add($"Day {day} daylight, last night, {playerDic[lastExiled].number} {playerDic[lastExiled].name} has been exiled!");
                    // 第三天被放逐的人才能留遗言
                    if (day > 1)
                    {
                        Console.WriteLine("Please leave your words before living the game.");
                        press1ToContinue();
                    }
                }
                randomGenerateATopic(playerDic, playback, day);
            }
            else
            {
                playback.Add($"Day 1 daylight, nothing to do but freely discuss.");
            }
            // 白天公投
            Console.WriteLine("Discussion Round start! Press 1 to continue to vote round!");
            press1ToContinue();
            if(day > 1)
            {
                int votedPlayerNumber = makingVoteRound(playerDic, playback, day);
                if (votedPlayerNumber != -1)
                {
                    Console.Clear();
                    Players votedPlayer = playerDic.ElementAt(votedPlayerNumber).Value;
                    if (votedPlayer.identity == Identities.Laity ||
                        votedPlayer.identity == Identities.Nicodemus ||
                        votedPlayer.identity == Identities.John ||
                        votedPlayer.identity == Identities.Peter)
                    {
                        Console.WriteLine("A Christian has been remove vote's weight!");
                        christianLostVote += votedPlayer.vote;
                    }
                    else
                    {
                        Console.WriteLine("A Judaism has been remove vote's weight!");
                        judaismLostVote += votedPlayer.vote;
                    }
                    playback.Add($"Day {day} daylight: {votedPlayer.number} {votedPlayer.name} has been remove vote's weight");
                    Console.WriteLine("({0} {1}) has been remove vote's weight!", votedPlayer.number, votedPlayer.name);
                    playerDic.ElementAt(votedPlayerNumber).Value.setVote(0);
                    
                }
                printPlayersInfo(playerDic.Values.ToArray());
            }
        }
        private static void assignPriestAndRulerOfTheSynagogue(Dictionary<string, Players> playerDic, List<string> playback)
        {
            Console.Clear();
            Random rand = new Random();
            int whoIsPriest = rand.Next(0, 2);
            if (whoIsPriest == 1)
            {
                // assign祭司
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Scribes");
                Console.ResetColor();
                Console.WriteLine(" is the Priest in this game!");
                Console.WriteLine();
                playerDic[Identities.Scribes.ToString()].setPriest();
                playback.Add($"Day 1 Night: {playerDic[Identities.Scribes.ToString()].number} {playerDic[Identities.Scribes.ToString()].name} is the Priest in this game!");

                // assign会堂管理者
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Pharisee");
                Console.ResetColor();
                Console.WriteLine(" is the Ruler Of The Synagogue in this game!");
                playerDic[Identities.Pharisee.ToString()].setRulerOfTheSynagogue();
                playback.Add($"Day 1 Night: {playerDic[Identities.Pharisee.ToString()].number} {playerDic[Identities.Pharisee.ToString()].name} is the Ruler Of The Synagogue in this game!");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Pharisee");
                Console.ResetColor();
                Console.WriteLine(" is the Priest in this game!");
                playerDic[Identities.Pharisee.ToString()].setPriest();
                playback.Add($"Day 1 Night: {playerDic[Identities.Pharisee.ToString()].number} {playerDic[Identities.Pharisee.ToString()].name} is the Priest in this game!");

                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Scribes");
                Console.ResetColor();
                Console.WriteLine(" is the Ruler Of The Synagogue in this game!");
                playerDic[Identities.Scribes.ToString()].setRulerOfTheSynagogue();
                playback.Add($"Day 1 Night: {playerDic[Identities.Scribes.ToString()].number} {playerDic[Identities.Scribes.ToString()].name} is the Ruler Of The Synagogue in this game!");
            }
        }
        private static void rulerOfTheSynagogueRound(Dictionary<string, Players> playerDic, List<string> playback, string lastExiled, int day)
        {
            Players rulerOfTheSynagogue = null!;
            Players[] players = playerDic.Values.ToArray();
            foreach (Players player in players)
            {
                if (player.rulerOfTheSynagogue)
                {
                    rulerOfTheSynagogue = player;
                    break;
                }
            }

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Ruler Of The Synagogue");
            Console.ResetColor();
            Console.WriteLine(" ({0} {1}) please open your eyes.", rulerOfTheSynagogue!.number, rulerOfTheSynagogue.name);
            Console.Write("Last night, the exiled person is ");

            if (rulerOfTheSynagogue!.inGame &&
                rulerOfTheSynagogue!.vote != 0 &&
                lastExiled != "" &&
                (lastExiled.Equals(Identities.Laity.ToString()) ||
                lastExiled.Equals(Identities.Nicodemus.ToString()) ||
                lastExiled.Equals(Identities.John.ToString()) ||
                lastExiled.Equals(Identities.Peter.ToString())))
            {
                Console.WriteLine("a christian!");
                playback.Add($"Day {day} Night: The Ruler Of The Synagogue knows that the last exilted person is Christian!");
            }
            else
            {
                playback.Add($"Day {day} Night: The Ruler Of The Synagogue did not get an answer!");
                Console.WriteLine("no answer or last night no one exiled!");
            }
        }
        private static void judasMeetPriest()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Judas");
            Console.ResetColor();
            Console.Write(" and ");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Priest");
            Console.ResetColor();
            Console.WriteLine(" open your eyes and disscuss.");
        }
        private static int nicodemusProtectionRound(Dictionary<string, Players> playerDic, List<string> playback, int exile, int day)
        {
            ConsoleKeyInfo choice1;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Nicodemus");
            Console.ResetColor();
            Console.Write(" please open your eyes.");
            Console.WriteLine();

            Console.Write("Tonight, ");
            // 如果exiled不是尼哥底母，尼哥底母有技能，就询问要不要保护。其他condition，直接跳过。
            if (playerDic.ElementAt(exile - 1).Value.identity != Identities.Nicodemus && 
                playerDic[Identities.Nicodemus.ToString()].nicodemusProtection)
            {
                Console.Write(playerDic.ElementAt(exile - 1).Value.number + " " + playerDic.ElementAt(exile - 1).Value.name);
                
                Console.Write(" is out. Are you going to save him/her?");
                Console.WriteLine();
                Console.WriteLine("Save -- 1, Not save -- 2");
                choice1 = Console.ReadKey(true);
                while (choice1.Key != ConsoleKey.D1 && choice1.Key != ConsoleKey.D2)
                {
                    Console.WriteLine("Save -- 1, Not save -- 2");
                    choice1 = Console.ReadKey(true);
                }
                if (choice1.Key == ConsoleKey.D1)
                {
                    playerDic[Identities.Nicodemus.ToString()].changeNicodemusProtection();
                    playback.Add($"Day {day} Night: Nicodemus saved his/her!");
                    return int.MinValue;
                }
                else
                {
                    playback.Add($"Day {day} Night: Nicodemus choose to not to save his/her!");
                    return exile;
                }
            }
            else if (playerDic.ElementAt(exile - 1).Value.identity == Identities.Nicodemus)
            {
                Console.WriteLine("The exiled person is Nicodemus. Nicodemus cannot out of game.");
                playback.Add($"Day {day} Night: Nicodemus cannot be exiled!");
                return int.MinValue;
            }
            else
            {
                Console.Write(" is out. Are you going to save him/her?");
                Console.WriteLine();
                playback.Add($"Day {day} Night: Nicodemus cannot use his ability!");
                return exile;
                
            }
        }
        private static void johnUseFire(Dictionary<string, Players> playerDic, Dictionary<string, Players> johnFireList, List<string> playback, int day)
        {
            bool quit = false;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("John");
            Console.ResetColor();
            Console.WriteLine(" please open your eyes.");
            if (playerDic[Identities.John.ToString()].inGame)
            {
                Console.WriteLine("Do you want to use your ability? Yes -- 1 No -- 2");
                ConsoleKeyInfo consoleKey = Console.ReadKey(true);
                while(consoleKey.Key != ConsoleKey.D1 && consoleKey.Key != ConsoleKey.D2) 
                {
                    Console.WriteLine("Do you want to use your ability? Yes -- 1 No -- 2");
                    consoleKey = Console.ReadKey(true);
                }
                if(consoleKey.Key == ConsoleKey.D1)
                {
                    simplePrintPlayerInfo(playerDic.Values.ToArray());
                    Players[] johnFireArray = johnFireList.Values.ToArray();
                    if (johnFireArray.Length != 0)
                    {
                        Console.WriteLine("Here is a list who has been already fired: ");
                        for (int i = 0; i < johnFireArray.Length; i++)
                        {
                            Console.WriteLine(johnFireArray[i].number + " " + johnFireArray[i].name);
                        }
                    }

                    Console.Write("(enter a number): ");
                    string key = Console.ReadLine()!;
                    int fire = tryPraseInput(key);

                    while (!quit)
                    {
                        if (fire < 1 || fire > playerDic.Count ||
                            playerDic.ElementAt(fire - 1).Key.Equals(Identities.John.ToString()) ||
                            playerDic.ElementAt(fire - 1).Value.inGame == false)
                        {
                            Console.WriteLine("Wrong number! Please enter a vaild value (1 ~ {0}), you cannot fire yourself and you cannot fire a person who was exiled!", playerDic.Count);
                            Console.Write("Please enter again: ");
                            key = Console.ReadLine()!;
                            fire = tryPraseInput(key);
                        }
                        else if (johnFireList.ContainsKey(playerDic.ElementAt(fire - 1).Key))
                        {
                            Console.WriteLine("Cannot fire the same person!");
                            Console.Write("Please enter again: ");
                            key = Console.ReadLine()!;
                            fire = tryPraseInput(key);
                        }
                        else
                        {
                            quit = true;
                        }
                    }
                    Players firePerson = playerDic.ElementAt(fire - 1).Value;
                    firePerson.setVote(firePerson.vote / 2);
                    johnFireList[firePerson.identity.ToString()] = firePerson;
                    playback.Add($"Day {day} Night: John fire {firePerson.number} {firePerson.name}!");
                }
                else
                {
                    Console.WriteLine("Please close your eyes.");
                    playback.Add($"Day {day} Night: John did not use his ability!");
                }
            }
            else
            {
                Console.WriteLine("Do you want to use your ability?");
                Console.WriteLine("Please close your eyes.");
                playback.Add($"Day {day} Night: John is out of game cannot use his ability!");
            }
        }
        public static int tryPraseInput(string key)
        {
            int number;
            while (!int.TryParse(key, out number))
            {
                Console.WriteLine("Please enter a valid number!");
                Console.Write("Please enter again: ");
                key = Console.ReadLine()!;
                Console.WriteLine();
            }
            return number;
        }
        private static void judasCheckRound(Dictionary<string, Players> playerDic, List<string> playback, int day)
        {
            bool quit = false;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Judas");
            Console.ResetColor();
            Console.Write(" please open your eyes.");
            Console.WriteLine();

            if (playerDic[Identities.Judas.ToString()].inGame && playerDic[Identities.Judas.ToString()].check)
            {
                simplePrintPlayerInfo(playerDic.Values.ToArray());
                Console.Write("Please decide which person you want to check (enter a number): ");
                string key = Console.ReadLine()!;
                int checkPerson = tryPraseInput(key);
                while (!quit)
                {
                    if (checkPerson < 1 ||
                        checkPerson > playerDic.Count ||
                        playerDic.ElementAt(checkPerson - 1).Key.Equals(Identities.Judas.ToString()) ||
                        playerDic.ElementAt(checkPerson - 1).Value.inGame == false)
                    {
                        Console.WriteLine("Wrong number! Please enter a vaild value (1 ~ {0}), you cannot check yourself and you cannot check a person who was exiled!", playerDic.Count);
                        Console.Write("Please enter again: ");
                        key = Console.ReadLine()!;
                        checkPerson = tryPraseInput(key);
                    }
                    else
                    {
                        quit = true;
                    }
                }
                string checkedPerson = playerDic.ElementAt(checkPerson - 1).Key;
                if (playerDic[checkedPerson].identity == Identities.Nicodemus ||
                    playerDic[checkedPerson].identity == Identities.John ||
                    playerDic[checkedPerson].identity == Identities.Peter ||
                    playerDic[checkedPerson].identity == Identities.Laity)
                {
                    playback.Add($"Day {day} Night: Judas check ---> {playerDic[checkedPerson].number} {playerDic[checkedPerson].name} and know he/she is a christian!");
                    Console.WriteLine("He/her is Christian!");
                }
                else
                {
                    playback.Add($"Day {day} Night: Judas check ---> {playerDic[checkedPerson].number} {playerDic[checkedPerson].name} and did not get an answer!");
                    Console.WriteLine("No answer!");
                }
            }
            else
            {
                playback.Add($"Day {day} Night: Judas is out of game or cannot use the ability!");
                Console.WriteLine("No answer!");
            }
        }
        private static int priestExileRound(Dictionary<string, Players> playerDic, List<string> playback, int day)
        {
            int exile = int.MinValue;
            Console.Clear();
            Players priest = null!;
            Players[] players = playerDic.Values.ToArray();
            foreach (Players player in players)
            {
                if (player.priest)
                {
                    priest = player;
                    break;
                }
            }
            Console.WriteLine("Priest ({0} {1}) please open your eyes.", priest!.number, priest.name);
            simplePrintPlayerInfo(playerDic.Values.ToArray());

            Console.Write("Please decide which person you want to exile (enter a number): ");
            bool quit = false;
            while (!quit)
            {
                string key = Console.ReadLine()!;
                exile = tryPraseInput(key);

                if (exile < 1 ||
                    exile > playerDic.Count ||
                    playerDic.ElementAt(exile - 1).Value.priest == true ||
                    playerDic.ElementAt(exile - 1).Value.inGame == false)
                {
                    Console.WriteLine("Wrong number! Please enter a vaild value (1 ~ {0}), you cannot exile yourself and you cannot exilt a person who has already been exiled!", playerDic.Count);
                    Console.Write("Please enter again: ");
                }
                else
                {
                    quit = true;
                }
            }
            playback.Add($"Day {day} Night: Priest wants to exile --> {playerDic.ElementAt(exile - 1).Value.number} {playerDic.ElementAt(exile - 1).Value.name}!");
            return exile;
        }
        private static void simplePrintPlayerInfo(Players[] players)
        {
            var table = new ConsoleTable("Number", "Name", "In Game");
            for (int i = 0; i < players.Length; i++)
            {
                List<string> row = new List<string>();
                row.Add(players[i].number.ToString());
                row.Add(players[i].name);
                if (players[i].inGame)
                {
                    row.Add("");
                }
                else
                {
                    row.Add("Out");
                }
                table.AddRow(row.ToArray());
            }
            table.Write(Format.Alternative);
        }
        /// <summary>
        /// Game process -> night round.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="playerDic"></param>
        /// <param name="playback"></param>
        /// <param name="lastExiled"></param>
        /// <returns> exiled player's number </returns>
        private static int night(int day, Dictionary<string, Players> playerDic, List<string> playback, string lastExiled, Dictionary<string, Players> johnFireList)
        {
            int exile;

            //print 夜间
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Day {0} night.", day);
            Console.ResetColor();
            Console.WriteLine();

            //如果是第一天夜间
            if (day == 1)
            {
                // 文士，法利赛人和尼哥底母见面
                scribesNicodemusPhariseeMeet();

                // 分配祭司和会堂管理者
                assignPriestAndRulerOfTheSynagogue(playerDic, playback);
                press1ToContinue();
            }
            else
            {
                // 会堂管理者时间
                rulerOfTheSynagogueRound(playerDic, playback, lastExiled, day);
                press1ToContinue();


                // 犹大和祭司见面
                judasMeetPriest();
                press1ToContinue();
            }

            //祭司放逐时间
            exile = priestExileRound(playerDic, playback, day);

            //尼哥底母时间
            exile = nicodemusProtectionRound(playerDic, playback, exile, day);
            press1ToContinue();

            //约翰天火技能时间
            johnUseFire(playerDic, johnFireList, playback, day);
            press1ToContinue();

            //犹大验人时间
            judasCheckRound(playerDic, playback, day);

            return exile;
        }
        private static void scribesNicodemusPhariseeMeet()
        {
            ConsoleKeyInfo choice1;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Scribes");
            Console.ResetColor();
            Console.Write(", ");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Pharisee");
            Console.ResetColor();
            Console.Write(" and ");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Nicodemus");
            Console.ResetColor();
            Console.WriteLine(" open their eyes.");
            Console.WriteLine("If finished, please ask them to close their eyes and press 1 to continue.");
            choice1 = Console.ReadKey(true);
            while (choice1.Key != ConsoleKey.D1)
            {
                Console.WriteLine("If finished, please ask them to close their eyes and press 1 to continue.");
                choice1 = Console.ReadKey(true);
            }
        }
        public static void processing(Dictionary<string, Players> playerDic, double totalVotes, List<string> playback)
        {
            string lastExiled = "";
            bool whoWins = true;
            
            int day = 1;
            int exile;
            Dictionary<string, Players> johnFireList = new Dictionary<string, Players>();

            for (int i = 0; i < playerDic.Count; i++)
            {
                Players players = playerDic.ElementAt(i).Value;
                if (players.identity == Identities.Judaism ||
                    players.identity == Identities.Judas ||
                    players.identity == Identities.Pharisee ||
                    players.identity == Identities.Scribes)
                {
                    judaismTotalVote += playerDic.ElementAt(i).Value.vote;
                }
            }

            while (whoWins)
            {
                daylight(day, playerDic, lastExiled, playback);

                whoWins = winCondition(totalVotes);
                if (whoWins)
                {
                    press1ToContinue();
                    exile = night(day, playerDic, playback, lastExiled, johnFireList);

                    // 如果有放逐的人
                    if (exile > 0)
                    {
                        string exileIdentity = playerDic.ElementAt(exile - 1).Value.identity.ToString();
                        // 如果放逐的人不是彼得或者约翰没有技能或者约翰不在场，放逐这个人
                        if (!(playerDic[exileIdentity].identity == Identities.Peter &&
                            playerDic[Identities.John.ToString()].johnProtection == true &&
                            playerDic[Identities.John.ToString()].inGame == true))
                        {
                            playerDic[exileIdentity].exileStatus(false);
                            lastExiled = exileIdentity;
                            // 放逐的人的权重
                            if (playerDic[exileIdentity].identity == Identities.Judaism ||
                                playerDic[exileIdentity].identity == Identities.Judas ||
                                playerDic[exileIdentity].identity == Identities.Pharisee ||
                                playerDic[exileIdentity].identity == Identities.Scribes)
                            {
                                judaismLostVote += playerDic[exileIdentity].vote;
                            }
                            else
                            {
                                christianLostVote += playerDic[exileIdentity].vote;
                            }
                        }
                        else
                        {

                            Console.WriteLine("John has protect peter!");
                            playerDic[Identities.John.ToString()].changeJohnProtection();
                            lastExiled = "";
                        }
                    }
                    else
                    {
                        lastExiled = "";
                    }
                    Console.WriteLine();
                    day++;
                    if (day == 3)
                    {
                        playerDic[Identities.Peter.ToString()].setVote(playerDic[Identities.Peter.ToString()].vote + 1);
                        Console.WriteLine("Day 3, Peter's vote increase by 1!");
                        playback.Add("Day 3, Peter's vote increase by 1!");
                    }
                    whoWins = winCondition(totalVotes);
                    press1ToContinue();
                }
            }
        }
    }
}
