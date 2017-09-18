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
            if (Game.state.Equals(EnumHandler.GameState.Game) && !Game.winScreen)
            {
                switch (key)
                {
                    case Keys.Escape:
                        if (down) Game.inPause = !Game.inPause;
                        break;
                }
            }
            #endregion
        }
    }
}
