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
            if (Game.state.Equals(EnumHandler.GameState.Game))
            {
                if (!Game.subState.Equals(EnumHandler.SubStates.Win) && down && !Game.subState.Equals(EnumHandler.SubStates.Loss))
                {
                    switch (key)
                    {
                        case Keys.Escape:
                            if (Game.subState.Equals(EnumHandler.SubStates.Pause) || Game.subState.Equals(EnumHandler.SubStates.None))
                            {
                                if (Game.subState.Equals(EnumHandler.SubStates.Pause)) Game.subState = EnumHandler.SubStates.None;
                                else Game.subState = EnumHandler.SubStates.Pause;
                            }
                            break;
                        case Keys.E:
                            if (Game.playerTurn && Game.subState.Equals(EnumHandler.SubStates.None)) Game.HandleAITurn();
                            break;
                    }
                }
                if (Game.subState.Equals(EnumHandler.SubStates.None))
                {
                    if (down)
                    {
                        switch (key)
                        {
                            case Keys.W:
                                Game.cameraYVel = -Game.cameraMoveSpeed;
                                break;
                            case Keys.S:
                                Game.cameraYVel = Game.cameraMoveSpeed;
                                break;
                            case Keys.A:
                                Game.cameraXVel = -Game.cameraMoveSpeed;
                                break;
                            case Keys.D:
                                Game.cameraXVel = Game.cameraMoveSpeed;
                                break;
                        }
                    }
                    else
                    {
                        switch (key)
                        {
                            case Keys.W:
                                if (Game.cameraYVel == -Game.cameraMoveSpeed) Game.cameraYVel = 0;
                                break;
                            case Keys.S:
                                if (Game.cameraYVel == Game.cameraMoveSpeed) Game.cameraYVel = 0;
                                break;
                            case Keys.A:
                                if (Game.cameraXVel == -Game.cameraMoveSpeed) Game.cameraXVel = 0;
                                break;
                            case Keys.D:
                                if (Game.cameraYVel == Game.cameraMoveSpeed) Game.cameraXVel = 0;
                                break;
                        }
                    }
                }
            }
            #endregion
        }
    }
}
