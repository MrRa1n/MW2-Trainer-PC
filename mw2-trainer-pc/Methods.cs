using System;
using System.Windows.Forms;
using PCAPI;

namespace mw2_trainer_pc
{
    class Methods
    {
        private static pcapi pc = new pcapi();
        private static byte[] chalBytes = null;
        private static byte[] titleBytes = null;
        private static byte[] emblemBytes = null;

        private static Random rnd = new Random();
        public static bool gameIsOpen = pc.ProcessHandle("iw4mp");
        //public static char keyPressed = Console.ReadKey().KeyChar;

        private static int score, wins, losses, ties, winstreak, kills, headshots, assists, killstreak, deaths;

        public static void Menu_Options()
        {
            Console_Welcome();
            Console.WriteLine(
                  "# 1. Unlock All Challenges, Titles and Emblems\n" 
                + "# 2. Set Custom Player Stats\n" 
                + "# 3. Set Prestige Level\n"
                + "# 4. Set Class Names"
                );
            char keyPressed = Console.ReadKey().KeyChar;

            if (gameIsOpen)
            {
                switch (keyPressed)
                {
                    case '1':
                        PerformUnlocks(chalBytes, 393, 0x0C, Addresses.AllChallenges);
                        PerformUnlocks(titleBytes, 40, byte.MaxValue, Addresses.AllTitles);
                        PerformUnlocks(emblemBytes, 71, byte.MaxValue, Addresses.AllEmblems);
                            MessageBox.Show("All items have been unlocked!");
                            Menu_Options();
                        break;

                    case '2':
                            Stats_Sub_Menu();
                        break;
                    case '3': // seems to work but maybe move to own method
                        Console_Welcome();
                            Console.Write("Enter Prestige Level: ");
                            pc.WriteInteger(Addresses.Prestige, Int32.Parse(Console.ReadLine()));
                            Console.Write("Enter Player XP: ");
                            pc.WriteInteger(Addresses.XP, int.Parse(Console.ReadLine()));
                        MessageBox.Show("Prestige and Level Set!");
                        Menu_Options();
                        break;
                    case '4':
                        RenameClasses();
                        Menu_Options();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nGame process is not running...");
                System.Threading.Thread.Sleep(2000);
                Menu_Options();
            }

            Console.ReadKey();
        }

        // complete this - create different methods
        private static void Stats_Sub_Menu()
        {
            Console_Welcome();

            Console.WriteLine(
                  "# 1. Insane Player Stats\n" 
                + "# 2. Legit Player Stats\n" 
                + "# 3. Low Player Stats\n"
                + "# 4. Custom Player Stats\n"
                );

            char keyPressed = Console.ReadKey().KeyChar;

            switch (keyPressed)
            {
                case '1':
                    SetInsanePlayerStats(1337, int.MaxValue);
                    Menu_Options();
                    break;
                case '2':
                    SetInsanePlayerStats(999, 99999999 / 50);
                    Menu_Options();
                    break;
                case '3':
                    SetInsanePlayerStats(999, 999999);
                    Menu_Options();
                    break;
                case '4':
                    SetCustomPlayerStats();
                    Menu_Options();
                    break;
            }
        }

        private static void PerformUnlocks(byte[] val, int size, byte val2, int address)
        {
            val = new byte[size];
            for (int i = 0; i < val.Length; i++)
                val[i] = val2;
            pc.WriteBytes(address, val);
        }

        private static void SetInsanePlayerStats(int low, int high)
        {
            pc.WriteInteger(Addresses.Score, rnd.Next(low, high));
            pc.WriteInteger(Addresses.Wins, rnd.Next(low, high));
            pc.WriteInteger(Addresses.Losses, rnd.Next(low, high) / 50);
            pc.WriteInteger(Addresses.Ties, rnd.Next(low, high) / 50);
            pc.WriteInteger(Addresses.Winstreak, rnd.Next(low, high));
            pc.WriteInteger(Addresses.Kills, rnd.Next(low, high));
            pc.WriteInteger(Addresses.Assists, rnd.Next(low, high));
            pc.WriteInteger(Addresses.Killstreak, rnd.Next(low, high));
            pc.WriteInteger(Addresses.Deaths, rnd.Next(low, high) / 50);
            pc.WriteInteger(Addresses.Headshots, rnd.Next(low, high));
        }

        private static void SetCustomPlayerStats()
        {
            Console.Clear();
            
            Console.Write("Player Score: ");
            IsCorrectStatValue(Addresses.Score, score);

            Console.Write("Player Wins: ");
            IsCorrectStatValue(Addresses.Wins, wins);

            Console.Write("Player Losses: ");
            IsCorrectStatValue(Addresses.Losses, losses);

            Console.Write("Player Ties: ");
            IsCorrectStatValue(Addresses.Ties, ties);

            Console.Write("Player Winstreak: ");
            IsCorrectStatValue(Addresses.Winstreak, winstreak);

            Console.Write("Player Kills: ");
            IsCorrectStatValue(Addresses.Kills, kills);

            Console.Write("Player Headshots: ");
            IsCorrectStatValue(Addresses.Headshots, headshots);

            Console.Write("Player Assists: ");
            IsCorrectStatValue(Addresses.Assists, assists);

            Console.Write("Player Killstreak: ");
            IsCorrectStatValue(Addresses.Killstreak, killstreak);

            Console.ReadLine();
        }

        private static void IsCorrectStatValue(int addr, int val)
        {
            bool valid_number = int.TryParse(Console.ReadLine(), out val);

            if (!valid_number)
            {
                MessageBox.Show("Please enter a value between 0 and " + int.MaxValue);
                SetCustomPlayerStats();
            }
            else
            {
                pc.WriteInteger(addr, val);
            }
        }

        private static void RenameClasses()
        {
            Console.Clear();
            Console.Write("Class Names: ");
            whatever(Addresses.classnames, Console.ReadLine());
        }

        private static void whatever(int[] addr, string cname)
        {
            
            for (int i = 0; i < 10; i++)
            {
                pc.WriteLongerNOP(addr[i]);
                pc.WriteString(addr[i], cname);
            }
        }

        private static void Console_Welcome()
        {
            Console.Clear();
            Console.WriteLine(">>> Welcome to MrRa1n MW2 Trainer! <<<" + "\n\nPlease select an option...\n");
        }
    }
}
