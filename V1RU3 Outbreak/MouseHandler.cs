using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace V1RU3_Outbreak
{
    public class MouseHandler
    {
        //define global variables
        public static int mouseX { get; set; } = 0;
        public static int mouseY { get; set; } = 0;
        public static Boolean mouseDown { get; set; } = false;

        //constructor
        public MouseHandler()
        {

        }

        //register mouse
        public void RegisterMouseEvent(int x, int y, Boolean down, MouseButtons button)
        {
            mouseDown = down;

            Graphics g = Graphics.FromImage(V1RU3_Outbreak.Properties.Resources.title);
            float widthScale = RenderingEngine.scaleX;
            float heightScale = RenderingEngine.scaleY;
            int width = RenderingEngine.canvasWidth;
            int height = RenderingEngine.canvasHeight;
            Font f = new Font(FontFamily.GenericSansSerif, 15 * Math.Min(widthScale, heightScale), FontStyle.Bold);
            Font fSmall = new Font(FontFamily.GenericSansSerif, 12 * Math.Min(widthScale, heightScale), FontStyle.Bold);
            float heightBaseForText = V1RU3_Outbreak.Properties.Resources.title.Height * heightScale + 30;

            #region MainMenu
            if (Game.state.Equals(EnumHandler.GameState.MainMenu) && !down)
            {
                if (mouseX >= RenderingEngine.canvasWidth / 2 - g.MeasureString("Play", f).Width / 2 && mouseX <= RenderingEngine.canvasWidth / 2 + g.MeasureString("Play", f).Width / 2)
                {
                    if (mouseY >= heightBaseForText + (20 * Math.Min(widthScale, heightScale)) && mouseY <= heightBaseForText + (20 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", f).Height)
                    {
                        Game.state = EnumHandler.GameState.Game;
                        if (Game.levelIndex >= Game.levelController.levels.Count)
                        {
                            Game.levelIndex = 0;
                            Game.turnsUsed = 0;
                            Game.subState = EnumHandler.SubStates.None;
                            Game.levelController = new LevelController();
                            RenderingEngine.textOnScreen = new List<String>();
                            RenderingEngine.textAddCycle = 0;
                            Game.CPUcycles = Game.maxCPUCycles;
                        }

                        Game.levelData = new LevelController().levels[Game.levelIndex];
                    }
                }

                if (mouseX >= RenderingEngine.canvasWidth / 2 - g.MeasureString("Options", f).Width / 2 && mouseX <= RenderingEngine.canvasWidth / 2 + g.MeasureString("Options", f).Width / 2)
                {
                    if (mouseY >= heightBaseForText + (60 * Math.Min(widthScale, heightScale)) && mouseY <= heightBaseForText + (60 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", f).Height)
                    {
                        Game.state = EnumHandler.GameState.OptionsMenu;
                    }
                }

                if (mouseX >= RenderingEngine.canvasWidth / 2 - g.MeasureString("Quit", f).Width / 2 && mouseX <= RenderingEngine.canvasWidth / 2 + g.MeasureString("Quit", f).Width / 2)
                {
                    if (mouseY >= heightBaseForText + (150 * Math.Min(widthScale, heightScale)) && mouseY <= heightBaseForText + (150 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", f).Height)
                    {
                        Application.Exit();
                    }
                }
            }
            #endregion

            #region OptionsMenu
            else if (Game.state.Equals(EnumHandler.GameState.OptionsMenu) && !down)
            {
                if (x <= RenderingEngine.canvasWidth / 2 - (200 * RenderingEngine.scaleX) / 2 
                    || x >= RenderingEngine.canvasWidth / 2 + ( 200 * RenderingEngine.scaleX) / 2 
                    || y <= RenderingEngine.canvasHeight / 2 - (200 * RenderingEngine.scaleY) / 2
                    || y >= RenderingEngine.canvasHeight / 2 + (200 * RenderingEngine.scaleY) / 2)
                {
                    Game.state = EnumHandler.GameState.MainMenu;
                }
            }
            #endregion

            #region Game
            else if (Game.state.Equals(EnumHandler.GameState.Game) && !down)
            {
                //menus
                if (Game.subState.Equals(EnumHandler.SubStates.Win))
                {
                    if (mouseX >= width / 2 - (40 * Math.Min(widthScale, heightScale)) && mouseX <= width / 2 - (40 * Math.Min(widthScale, heightScale)) + g.MeasureString("Next Level ->", fSmall).Width)
                    {
                        if (mouseY >= height / 2 + (50 * Math.Min(widthScale, heightScale)) && mouseY <= height / 2 + (50 * Math.Min(widthScale, heightScale)) + g.MeasureString("Next Level ->", fSmall).Height)
                        {
                            Game.levelIndex++;
                            if (Game.levelIndex < Game.levelController.levels.Count)
                            {
                                Game.levelData = Game.levelController.levels[Game.levelIndex];
                                Game.turnsUsed = 0;
                                Game.subState = EnumHandler.SubStates.None;
                                RenderingEngine.screenFade = 255;
                                RenderingEngine.textOnScreen = new List<String>();
                                RenderingEngine.textAddCycle = 0;
                                RenderingEngine.menuDropInCycle = 0;
                            }
                            else
                            {
                                Game.state = EnumHandler.GameState.MainMenu;
                            }
                        }
                    }

                    if (mouseX >= width / 2 - (85 * Math.Min(widthScale, heightScale)) && mouseX <= width / 2 - (85 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Width)
                    {
                        if (mouseY >= height / 2 + (50 * Math.Min(widthScale, heightScale)) && mouseY <= height / 2 + (50 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Height)
                        {
                            Game.state = EnumHandler.GameState.MainMenu;
                        }
                    }
                }

                if (Game.subState.Equals(EnumHandler.SubStates.Loss))
                {
                    if (mouseX >= width / 2 - (10 * Math.Min(widthScale, heightScale)) && mouseX <= width / 2 - (10 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart", f).Width)
                    {
                        if (mouseY >= height / 2 + (50 * Math.Min(widthScale, heightScale)) && mouseY <= height / 2 + (50 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart", f).Height)
                        {
                            Game.RestartGame();
                        }
                    }

                    if (mouseX >= width / 2 - (85 * Math.Min(widthScale, heightScale)) && mouseX <= width / 2 - (85 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Width)
                    {
                        if (mouseY >= height / 2 + (50 * Math.Min(widthScale, heightScale)) && mouseY <= height / 2 + (50 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Height)
                        {
                            Game.state = EnumHandler.GameState.MainMenu;
                            Game.subState = EnumHandler.SubStates.None;
                        }
                    }
                }

                if (Game.subState.Equals(EnumHandler.SubStates.Pause))
                {
                    float heightBase = (height / 2 + (50 * heightScale) / 2) - (125 * Math.Min(widthScale, heightScale));

                    if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Restart", f).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Restart", f).Width / 2)
                    {
                        if (MouseHandler.mouseY >= heightBase + (10 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= heightBase + (10 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart", f).Height)
                        {
                            Game.RestartGame();
                        }
                    }

                    if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Main Menu", f).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Main Menu", f).Width / 2)
                    {
                        if (MouseHandler.mouseY >= heightBase + 30 * Math.Min(widthScale, heightScale) && MouseHandler.mouseY <= heightBase + 30 * Math.Min(widthScale, heightScale) + g.MeasureString("Main Menu", f).Height)
                        {
                            Game.state = EnumHandler.GameState.MainMenu;
                        }
                    }

                    if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Exit To Desktop", f).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Exit To Desktop", f).Width / 2)
                    {
                        if (MouseHandler.mouseY >= heightBase + 100 * Math.Min(widthScale, heightScale) && MouseHandler.mouseY <= heightBase + 100 * Math.Min(widthScale, heightScale) + g.MeasureString("Exit To Desktop", f).Height)
                        {
                            Application.Exit();
                        }
                    }
                }

                if (Game.playerTurn && RenderingEngine.screenFade <= 150 && Game.subState.Equals(EnumHandler.SubStates.None))
                {
                    //hud
                    if (mouseX >= 32 * widthScale - 3 && mouseX <= 32 * widthScale - 3 + (30 * widthScale))
                    {
                        if (mouseY >= 5 * heightScale && mouseY <= 5 * heightScale + (15 * heightScale))
                        {
                            if(Game.playerTurn) Game.HandleAITurn();
                        }
                    }


                    //blocks
                    float tileSize = 15 * Math.Min(widthScale, heightScale);
                    float baseX = width / 2 - (Game.levelData.gridSize * tileSize) / 2;
                    float baseY = height / 2 - (Game.levelData.gridSize * tileSize) / 2;

                    float newX = x - baseX;
                    float newY = y - baseY;

                    newX /= tileSize;
                    newX = (float)Math.Ceiling(newX);

                    newY /= tileSize;
                    newY = (float)Math.Ceiling(newY);

                    if (newX > 0 && newX < Game.levelData.gridSize + 1 && !Game.blockPlaced)
                    {
                        if (newY > 0 && newY < Game.levelData.gridSize + 1)
                        {
                            Boolean pass = true;

                            foreach (Virus s in Game.levelData.viruses)
                            {
                                if (s.x == newX && s.y == newY)
                                {
                                    pass = false;
                                    break;
                                }
                            }

                            foreach (Block b in Game.levelData.blocks.Concat(Game.levelData.corruption).Concat(Game.levelData.importantData))
                            {
                                if (b.x == newX && b.y == newY)
                                {
                                    pass = false;
                                    break;
                                }
                            }

                            if (pass)
                            {
                                //place block
                                Game.levelData.blocks.Add(new Block((int)newX, (int)newY));
                                Game.blockPlaced = true;
                            }
                        }
                    }
                }
            }
            #endregion
        }

        //register mouse move
        public void RegisterMouseMove(int x, int y)
        {
            mouseX = x;
            mouseY = y;
        }
    }
}
