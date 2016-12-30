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

        public static bool gameIsOpen = pc.ProcessHandle("iw4mp");

        public static void Menu_Options()
        {
            Console_Welcome();
            Console.WriteLine(
                  "# 1. Unlock All Challenges, Titles and Emblems\n" 
                + "# 2. Set Custom Player Stats\n" 
                + "# 3. Set Prestige Level\n"
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
                            pc.WriteInteger(Addresses.Prestige, UInt32.Parse(Console.ReadLine()));
                        MessageBox.Show("Prestige Level Set!");
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

        }

        private static void PerformUnlocks(byte[] val, int size, byte val2, uint address)
        {
            val = new byte[size];
            for (int i = 0; i < val.Length; i++)
                val[i] = val2;
            pc.WriteBytes(address, val);
        }

        private static void SetPlayerStats(uint value)
        {
            pc.WriteInteger(Addresses.XP, value);
            pc.WriteInteger(Addresses.Score, value);
            pc.WriteInteger(Addresses.Kills, value);
            pc.WriteInteger(Addresses.Deaths, value / 200);
            pc.WriteInteger(Addresses.Wins, value);
            pc.WriteInteger(Addresses.Losses, value / 350);
            pc.WriteInteger(Addresses.Headshots, value);
            pc.WriteInteger(Addresses.Ties, value / 4);
            pc.WriteInteger(Addresses.Winstreak, value);
            pc.WriteInteger(Addresses.Assists, value / 3);
            pc.WriteInteger(Addresses.Killstreak, value);
        }

        private static void Console_Welcome()
        {
            Console.Clear();
            Console.WriteLine(">>> Welcome to MrRa1n MW2 Trainer! <<<" + "\n\nPlease select an option...\n");
        }
    }
}
