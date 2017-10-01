using System;

namespace V1RU3_Outbreak
{
    public class EnumHandler
    {
        //define enums
        public enum GameState
        {
            MainMenu,
            OptionsMenu,
            Game
        }

        public enum SubStates
        {
            None,
            Pause,
            Win,
            Loss,
            Puzzle,
            Shop
        }

        public enum PuzzleTypes
        {
            None,
            Pipes,
            Binary,
            Matrix
        }

        public enum Items
        {
            Antivirus,
            Firewall,
            PCUpgrade1,
            PCUpgrade2,
            PCUpgrade3
        }
    }
}
