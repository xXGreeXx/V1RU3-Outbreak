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
            Game,
            LevelSelect
        }

        public enum SubStates
        {
            None,
            Pause,
            Win,
            GameWin,
            Loss,
            Puzzle,
            Shop,
            Tutorial
        }

        public enum PuzzleTypes
        {
            None,
            Pipes,
            Binary,
            Matrix,
            Encryption,
            Swap
        }

        public enum Items
        {
            Antivirus,
            Firewall,
            DiskDefragger,
            DataEncrypter,
            Sandbox,
            AntiMalware,
            PCUpgrade1,
            PCUpgrade2,
            PCUpgrade3,
            CPUSpeed1,
            CPUSpeed2
        }

        public enum VirusTypes
        {
            Green,
            Black,
            Red,
            Orange,
            Yellow
        }
    }
}
