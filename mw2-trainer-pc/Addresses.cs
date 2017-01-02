namespace mw2_trainer_pc
{
    class Addresses
    {
        // Player Stat Addresses
        public static int
            XP = 0x01B8B768,
            Score = 0x01B8B778,
            Kills = 0x01B8B77C,
            Deaths = 0x01B8B784,
            Wins = 0x01B8B7B0,
            Losses = 0x01B8B7B4,
            Headshots = 0x01B8B790,
            Ties = 0x01B8B7B8,
            Winstreak = 0x01B8B7BC,
            Assists = 0x01B8B78C,
            Killstreak = 0x01B8B780,
            Prestige = 0x01B8B770,

            AllChallenges = 0x1B8BDF0,
            AllTitles = 0x1B8C8C7,
            AllEmblems = 0x1B8C947;

        // Class Name Addresses
        public static int
            Class1 = 0x01B8BB40,
            Class2 = 0x01B8BB80,
            Class3 = 0x01B8BBC0,
            Class4 = 0x01B8BC00,
            Class5 = 0x01B8BC40,
            Class6 = 0x01B8BC80,
            Class7 = 0x01B8BCC0,
            Class8 = 0x01B8BD00,
            Class9 = 0x01B8BD40,
            Class10 = 0x01B8BD80;

        public static int[] classnames = { Class1, Class2, Class3,
        Class4, Class5, Class6, Class7, Class8, Class9, Class10 };
    }
}
