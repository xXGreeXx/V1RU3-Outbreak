using System;
using System.Windows.Forms;

namespace V1RU3_Outbreak
{
    public class KeyboardHandler
    {
        //define global variables

        //constructor
        public KeyboardHandler()
        {

        }

        //register key event
        public void RegisterKeyEvent(Keys key, Boolean down)
        {
            #region OptionsMenu
            if (Game.state.Equals(EnumHandler.GameState.OptionsMenu))
            {
                switch (key)
                {
                    case Keys.Escape:
                        if (down) Game.state = EnumHandler.GameState.MainMenu;
                        break;
                }
            }
            #endregion

            #region Game
            if (Game.state.Equals(EnumHandler.GameState.Game) && !Game.subState.Equals(EnumHandler.SubStates.Win) && down && !Game.subState.Equals(EnumHandler.SubStates.Loss))
            {
                switch (key)
                {
                    case Keys.Escape:
                        if (Game.subState.Equals(EnumHandler.SubStates.Pause)) Game.subState = EnumHandler.SubStates.None;
                        else Game.subState = EnumHandler.SubStates.Pause;
                        break;
                }
            }
            #endregion
        }
    }
}
