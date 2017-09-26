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
            Loss
        }

        public enum PuzzleTypes
        {
            Pipes,
            Binary,
            Matrix
        }
    }
}
