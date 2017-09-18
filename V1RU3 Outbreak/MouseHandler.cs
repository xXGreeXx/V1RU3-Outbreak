using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
            Font fontForText = new Font(FontFamily.GenericSansSerif, 15 * Math.Min(widthScale, heightScale), FontStyle.Bold);
            float heightBaseForText = V1RU3_Outbreak.Properties.Resources.title.Height * heightScale + 30;

            //main menu
            if (Game.state.Equals(EnumHandler.GameState.MainMenu) && !down)
            {
                if (mouseX >= RenderingEngine.canvasWidth / 2 - g.MeasureString("Play", fontForText).Width / 2 && mouseX <= RenderingEngine.canvasWidth / 2 + g.MeasureString("Play", fontForText).Width / 2)
                {
                    if (mouseY >= heightBaseForText + (20 * Math.Min(widthScale, heightScale)) && mouseY <= heightBaseForText + (20 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", fontForText).Height)
                    {
                        Game.state = EnumHandler.GameState.Game;
                    }
                }

                if (mouseX >= RenderingEngine.canvasWidth / 2 - g.MeasureString("Options", fontForText).Width / 2 && mouseX <= RenderingEngine.canvasWidth / 2 + g.MeasureString("Options", fontForText).Width / 2)
                {
                    if (mouseY >= heightBaseForText + (60 * Math.Min(widthScale, heightScale)) && mouseY <= heightBaseForText + (60 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", fontForText).Height)
                    {
                        Game.state = EnumHandler.GameState.OptionsMenu;
                    }
                }

                if (mouseX >= RenderingEngine.canvasWidth / 2 - g.MeasureString("Quit", fontForText).Width / 2 && mouseX <= RenderingEngine.canvasWidth / 2 + g.MeasureString("Quit", fontForText).Width / 2)
                {
                    if (mouseY >= heightBaseForText + (150 * Math.Min(widthScale, heightScale)) && mouseY <= heightBaseForText + (150 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", fontForText).Height)
                    {
                        Application.Exit();
                    }
                }
            }

            //options menu
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
            
            //game
            else if (Game.state.Equals(EnumHandler.GameState.Game) && !down)
            {
                if (Game.playerTurn && RenderingEngine.screenFade <= 50)
                {
                    float tileSize = 15 * Math.Min(widthScale, heightScale);
                    float baseX = width / 2 - (Game.levelData.gridSize * tileSize) / 2;
                    float baseY = height / 2 - (Game.levelData.gridSize * tileSize) / 2;

                    float newX = x - baseX;
                    float newY = y - baseY;

                    newX /= tileSize;
                    newX = (float)Math.Ceiling(newX);

                    newY /= tileSize;
                    newY = (float)Math.Ceiling(newY);

                    if (newX > 0 && newX < Game.levelData.gridSize + 1)
                    {
                        if (newY > 0 && newY < Game.levelData.gridSize + 1)
                        {
                            //place block
                            Game.levelData.blocks.Add(new Block((int)newX, (int)newY));
                            Game.playerTurn = false;

                            //handle ai
                            AI ai = new AI();
                            foreach (Virus v in ai.SimulateAI(Game.levelData))
                            {
                                Game.levelData.viruses.Add(v);
                            }
                            Game.playerTurn = true;
                        }
                    }
                }
            }
        }

        //register mouse move
        public void RegisterMouseMove(int x, int y)
        {
            mouseX = x;
            mouseY = y;
        }
    }
}
