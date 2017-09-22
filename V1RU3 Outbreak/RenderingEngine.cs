using System;
using System.Collections.Generic;
using System.Drawing;

namespace V1RU3_Outbreak
{
    public class RenderingEngine
    {
        //define global variables
        Bitmap title = V1RU3_Outbreak.Properties.Resources.title;
        Bitmap background = V1RU3_Outbreak.Properties.Resources.background;
        Bitmap corruption = V1RU3_Outbreak.Properties.Resources.corruption;
        Bitmap virus = V1RU3_Outbreak.Properties.Resources.virus;
        Bitmap board = V1RU3_Outbreak.Properties.Resources.board;
        Bitmap partition = V1RU3_Outbreak.Properties.Resources.partition;
        Bitmap importantData = V1RU3_Outbreak.Properties.Resources.importantData;
        Bitmap pauseIcon = V1RU3_Outbreak.Properties.Resources.pauseIcon;

        public static float scaleX { get; set; } = 1;
        public static float scaleY { get; set; } = 1;

        public static int canvasWidth { get; set; } = 0;
        public static int canvasHeight { get; set; } = 0;

        private float rotation = 0;
        public static int screenFade { get; set; } = 255;

        public static List<String> textOnScreen { get; set; } = new List<String>();
        public static int textAddCycle { get; set; } = 0;

        //constructor
        public RenderingEngine()
        {

        }

        //draw main menu
        public void DrawMainMenu(Graphics g, int width, int height, float widthScale, float heightScale)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            scaleX = widthScale;
            scaleY = heightScale;

            canvasWidth = width;
            canvasHeight = height;

            //draw menu background
            rotation += 1;

            float tileSize = (350 / Game.levelData.gridSize) * Math.Min(widthScale, heightScale);
            float gridSize = Game.levelData.gridSize * tileSize;

            float tileOffsetX = 0;
            float tileOffsetY = 0;

            float gridOffset = 10 * Math.Min(widthScale, heightScale);

            g.TranslateTransform((width / 2) + tileOffsetX, (height / 2) + tileOffsetY);
            g.RotateTransform(rotation);
            g.TranslateTransform(-((width / 2) + tileOffsetX), -((height / 2) + tileOffsetY));

            g.DrawImage(board, width / 2 - (gridSize / 2), height / 2 - (gridSize / 2) - gridOffset, gridSize, gridSize);

            for (float x = width / 2 - (gridSize / 2); x < width / 2 + (gridSize / 2) - tileSize / 2; x += tileSize)
            {
                for (float  y = height / 2 - (gridSize / 2) - gridOffset; y < height / 2 + (gridSize / 2) - tileSize / 2 - gridOffset; y += tileSize)
                {
                    g.DrawRectangle(Pens.Black, x, y, tileSize, tileSize);
                }
            }

            foreach (Block b in Game.levelData.blocks)
            {
                g.DrawImage(partition, width / 2 - (gridSize / 2) + ((b.x - 1) * tileSize), height / 2 - (gridSize / 2) - gridOffset + ((b.y - 1) * tileSize), tileSize, tileSize);
            }

            foreach (Block b in Game.levelData.corruption)
            {
                g.DrawImage(corruption, width / 2 - (gridSize / 2) + ((b.x - 1) * tileSize), height / 2 - (gridSize / 2) - gridOffset + ((b.y - 1) * tileSize), tileSize, tileSize);
            }

            foreach (Virus v in Game.levelData.viruses)
            {
                g.DrawImage(virus, width / 2 - (gridSize / 2) + ((v.x - 1) * tileSize), height / 2 - (gridSize / 2) - gridOffset + ((v.y - 1) * tileSize), tileSize, tileSize);
            }

            foreach (Block b in Game.levelData.importantData)
            {
                g.DrawImage(importantData, width / 2 - (gridSize / 2) + ((b.x - 1) * tileSize), height / 2 - (gridSize / 2) - gridOffset + ((b.y - 1) * tileSize), tileSize, tileSize);
            }

            g.TranslateTransform((width / 2) + tileOffsetX, (height / 2) + tileOffsetY);
            g.RotateTransform(-rotation);
            g.TranslateTransform(-((width / 2) + tileOffsetX), -((height / 2) + tileOffsetY));

            //draw title
            float widthOfTitle = title.Width * widthScale;
            g.DrawImage(title, width / 2 - widthOfTitle / 2, 10, title.Width * widthScale, title.Height * heightScale);

            //draw text
            Font fontForText = new Font(FontFamily.GenericSansSerif, 15 * Math.Min(widthScale, heightScale), FontStyle.Bold);

            float heightBaseForText = title.Height * heightScale + 30;
            g.DrawString("Play", fontForText, Brushes.Black, width / 2 - g.MeasureString("Play", fontForText).Width / 2, heightBaseForText + (20 * Math.Min(widthScale, heightScale)));
            g.DrawString("Options", fontForText, Brushes.Black, width / 2 - g.MeasureString("Options", fontForText).Width / 2, heightBaseForText + (60 * Math.Min(widthScale, heightScale)));
            g.DrawString("Quit", fontForText, Brushes.Black, width / 2 - g.MeasureString("Quit", fontForText).Width / 2, heightBaseForText + (150 * Math.Min(widthScale, heightScale)));

            if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Play", fontForText).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Play", fontForText).Width / 2)
            {
                if (MouseHandler.mouseY >= heightBaseForText + (20 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= heightBaseForText + (20 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", fontForText).Height)
                {
                    if(!MouseHandler.mouseDown) g.DrawString("Play", fontForText, Brushes.LightGray, width / 2 - g.MeasureString("Play", fontForText).Width / 2, heightBaseForText + (20 * Math.Min(widthScale, heightScale)));
                    else g.DrawString("Play", fontForText, Brushes.DarkGray, width / 2 - g.MeasureString("Play", fontForText).Width / 2, heightBaseForText + (20 * Math.Min(widthScale, heightScale)));
                }
            }

            if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Options", fontForText).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Options", fontForText).Width / 2)
            {
                if (MouseHandler.mouseY >= heightBaseForText + (60 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= heightBaseForText + (60 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", fontForText).Height)
                {
                    if(!MouseHandler.mouseDown) g.DrawString("Options", fontForText, Brushes.LightGray, width / 2 - g.MeasureString("Options", fontForText).Width / 2, heightBaseForText + (60 * Math.Min(widthScale, heightScale)));
                    else g.DrawString("Options", fontForText, Brushes.DarkGray, width / 2 - g.MeasureString("Options", fontForText).Width / 2, heightBaseForText + (60 * Math.Min(widthScale, heightScale)));
                }
            }

            if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Quit", fontForText).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Quit", fontForText).Width / 2)
            {
                if (MouseHandler.mouseY >= heightBaseForText + (150 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= heightBaseForText + (150 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", fontForText).Height)
                {
                    if(!MouseHandler.mouseDown) g.DrawString("Quit", fontForText, Brushes.LightGray, width / 2 - g.MeasureString("Quit", fontForText).Width / 2, heightBaseForText + (150 * Math.Min(widthScale, heightScale)));
                    else g.DrawString("Quit", fontForText, Brushes.DarkGray, width / 2 - g.MeasureString("Quit", fontForText).Width / 2, heightBaseForText + (150 * Math.Min(widthScale, heightScale)));
                }
            }
        }

        //draw options menu
        public void DrawOptionsMenu(Graphics g, int width, int height, float widthScale, float heightScale)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            scaleX = widthScale;
            scaleY = heightScale;

            canvasWidth = width;
            canvasHeight = height;

            //draw menu background
            rotation += 1;

            float tileSize = (350 / Game.levelData.gridSize) * Math.Min(widthScale, heightScale);
            float gridSize = Game.levelData.gridSize * tileSize;

            float tileOffsetX = 0;
            float tileOffsetY = 0;

            float gridOffset = 10 * Math.Min(widthScale, heightScale);

            g.TranslateTransform((width / 2) + tileOffsetX, (height / 2) + tileOffsetY);
            g.RotateTransform(rotation);
            g.TranslateTransform(-((width / 2) + tileOffsetX), -((height / 2) + tileOffsetY));

            g.DrawImage(board, width / 2 - (gridSize / 2), height / 2 - (gridSize / 2) - gridOffset, gridSize, gridSize);

            for (float x = width / 2 - (gridSize / 2); x < width / 2 + (gridSize / 2) - tileSize / 2; x += tileSize)
            {
                for (float y = height / 2 - (gridSize / 2) - gridOffset; y < height / 2 + (gridSize / 2) - tileSize / 2 - gridOffset; y += tileSize)
                {
                    g.DrawRectangle(Pens.Black, x, y, tileSize, tileSize);
                }
            }

            foreach (Block b in Game.levelData.blocks)
            {
                g.DrawImage(partition, width / 2 - (gridSize / 2) + ((b.x - 1) * tileSize), height / 2 - (gridSize / 2) - gridOffset + ((b.y - 1) * tileSize), tileSize, tileSize);
            }

            foreach (Block b in Game.levelData.corruption)
            {
                g.DrawImage(corruption, width / 2 - (gridSize / 2) + ((b.x - 1) * tileSize), height / 2 - (gridSize / 2) - gridOffset + ((b.y - 1) * tileSize), tileSize, tileSize);
            }

            foreach (Virus v in Game.levelData.viruses)
            {
                g.DrawImage(virus, width / 2 - (gridSize / 2) + ((v.x - 1) * tileSize), height / 2 - (gridSize / 2) - gridOffset + ((v.y - 1) * tileSize), tileSize, tileSize);
            }

            foreach (Block b in Game.levelData.importantData)
            {
                g.DrawImage(importantData, width / 2 - (gridSize / 2) + ((b.x - 1) * tileSize), height / 2 - (gridSize / 2) - gridOffset + ((b.y - 1) * tileSize), tileSize, tileSize);
            }

            g.TranslateTransform((width / 2) + tileOffsetX, (height / 2) + tileOffsetY);
            g.RotateTransform(-rotation);
            g.TranslateTransform(-((width / 2) + tileOffsetX), -((height / 2) + tileOffsetY));

            //draw title
            float widthOfTitle = title.Width * widthScale;
            g.DrawImage(title, width / 2 - widthOfTitle / 2, 10, title.Width * widthScale, title.Height * heightScale);

            //draw rectangle for options
            g.DrawImage(background, width / 2 - (200 * Math.Min(widthScale, heightScale)) / 2, height / 2 - (200 * Math.Min(widthScale, heightScale)) / 2, 200 * Math.Min(widthScale, heightScale), 200 * Math.Min(widthScale, heightScale));

            //draw options
            Font fontForText = new Font(FontFamily.GenericSansSerif, 10 * Math.Min(widthScale, heightScale), FontStyle.Bold);
            g.DrawString("Fullscreen: ", fontForText, Brushes.Black, width / 2 - (190 * Math.Min(widthScale, heightScale)) / 2, height / 2 - (190 * Math.Min(widthScale, heightScale)) / 2);
        }

        //draw game
        public void DrawGame(Graphics g, int width, int height, float widthScale, float heightScale, LevelData level)
        {
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            scaleX = widthScale;
            scaleY = heightScale;

            canvasWidth = width;
            canvasHeight = height;

            //define variables
            float tileSize = 15 * Math.Min(widthScale, heightScale);
            float baseX = width / 2 - (level.gridSize * tileSize) / 2;
            float baseY = height / 2 - (level.gridSize * tileSize) / 2;
            Font f = new Font(FontFamily.GenericSansSerif, 15 * Math.Min(widthScale, heightScale), FontStyle.Regular | FontStyle.Bold);
            Font fSmall = new Font(FontFamily.GenericMonospace, 12 * Math.Min(widthScale, heightScale), FontStyle.Regular | FontStyle.Bold);
            Font fLarge = new Font(FontFamily.GenericSansSerif, 25 * Math.Min(widthScale, heightScale), FontStyle.Bold | FontStyle.Underline);

            g.DrawImage(board, baseX, baseY, level.gridSize * tileSize, level.gridSize * tileSize);

            //draw viruses
            foreach (Virus v in level.viruses)
            {
                v.frame += 0.1F;
                if(SetImageAnimationFrame(virus, v.frame)) v.frame = 0;

                g.DrawImage(virus, baseX + ((v.x - 1) * tileSize), baseY + ((v.y - 1) * tileSize), tileSize, tileSize);
            }

            //draw blocks
            foreach (Block b in level.blocks)
            {
                g.DrawImage(partition, baseX + ((b.x - 1) * tileSize), baseY + ((b.y - 1) * tileSize), tileSize, tileSize);
            }

            //draw corruption
            foreach (Block b in level.corruption)
            {
                g.DrawImage(corruption, baseX + ((b.x - 1) * tileSize), baseY + ((b.y - 1) * tileSize), tileSize, tileSize);
            }

            //draw important data
            foreach(Block b in level.importantData)
            {
                g.DrawImage(importantData, baseX + ((b.x - 1) * tileSize), baseY + ((b.y - 1) * tileSize), tileSize, tileSize);
            }

            //draw base grid of level
            for (float x = baseX; x < baseX + level.gridSize * tileSize - tileSize / 2; x += tileSize)
            {
                for (float y = baseY; y < baseY + level.gridSize * tileSize - tileSize / 2; y += tileSize)
                {
                    g.DrawRectangle(Pens.Black, x, y, tileSize, tileSize);
                }
            }

            //draw fade
            int fadeOffs = 4;
            if (screenFade - fadeOffs > 0)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(screenFade, Color.Black));
                g.FillRectangle(brush, 0, 0, width, height);
                screenFade -= fadeOffs;
            }

            //draw win screen
            if (Game.subState.Equals(EnumHandler.SubStates.Win))
            {
                g.DrawImage(background, width / 2 - (100 * Math.Min(widthScale, heightScale)), height / 2 - (100 * Math.Min(widthScale, heightScale)), 200 * Math.Min(widthScale, heightScale), 190 * Math.Min(widthScale, heightScale));
                g.DrawString("Complete!", fLarge, Brushes.Black, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-100 * Math.Min(widthScale, heightScale)));
                g.DrawString("Complete!", fLarge, Brushes.DarkGray, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-98 * Math.Min(widthScale, heightScale)));

                //Game.particleEngine.GenerateExplosion(10, width / 2 - (85 * Math.Min(widthScale, heightScale)) + Game.r.Next(0, 160 * (int)Math.Min(widthScale, heightScale)), height / 2 + (-100 * Math.Min(widthScale, heightScale)), 30, 100, Color.Lime);

                int yOffset = 0;
                foreach (String s in textOnScreen)
                {
                    g.DrawString(s, fSmall, Brushes.Black, width / 2 - (83 * Math.Min(widthScale, heightScale)), height / 2 + (-150 + yOffset * Math.Min(widthScale, heightScale)));
                    yOffset += 25;
                }

                if(textAddCycle < 45) textAddCycle++;
                if (textAddCycle == 10)
                {
                    textOnScreen.Add("Turns Used: " + Game.turnsUsed);
                    Game.particleEngine.GenerateExplosion(10, width / 2 - (83 * Math.Min(widthScale, heightScale)), height / 2 + (-150 + 0 * Math.Min(widthScale, heightScale)), 200, 430, Color.Black);
                }
                if (textAddCycle == 25)
                {
                    textOnScreen.Add("Viruses: " + Game.levelData.viruses.Count);
                    Game.particleEngine.GenerateExplosion(10, width / 2 - (83 * Math.Min(widthScale, heightScale)), height / 2 + (-150 + 25 * Math.Min(widthScale, heightScale)), 200, 450, Color.Black);
                }
                if (textAddCycle == 40)
                {
                    textOnScreen.Add("Data Saved: " + Game.levelData.importantData.Count + "/" + new LevelController().levels[Game.levelIndex].importantData.Count);
                    Game.particleEngine.GenerateExplosion(10, width / 2 - (83 * Math.Min(widthScale, heightScale)), height / 2 + (-150 + 50 * Math.Min(widthScale, heightScale)), 200, 500, Color.Black);
                    textAddCycle++;
                }


                g.DrawString("Next Level ->", f, Brushes.Black, width / 2 - (40 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
                g.DrawString("Back", fSmall, Brushes.Black, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));

                if (MouseHandler.mouseX >= width / 2 - (40 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseX <= width / 2 - (40 * Math.Min(widthScale, heightScale)) + g.MeasureString("Next Level ->", f).Width)
                {
                    if (MouseHandler.mouseY >= height / 2 + (50 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= height / 2 + (50 * Math.Min(widthScale, heightScale)) + g.MeasureString("Next Level ->", f).Height)
                    {
                        if(MouseHandler.mouseDown) g.DrawString("Next Level ->", f, Brushes.White, width / 2 - (40 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
                        else g.DrawString("Next Level ->", f, Brushes.DarkGray, width / 2 - (40 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
                    }
                }

                if (MouseHandler.mouseX >= width / 2 - (85 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseX <= width / 2 - (85 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Width)
                {
                    if (MouseHandler.mouseY >= height / 2 + (50 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= height / 2 + (50 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Height)
                    {
                        if (MouseHandler.mouseDown) g.DrawString("Back", fSmall, Brushes.White, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
                        else g.DrawString("Back", fSmall, Brushes.DarkGray, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
                    }
                }
            }

            //draw loss screen
            if (Game.subState.Equals(EnumHandler.SubStates.Loss))
            {
                g.DrawImage(background, width / 2 - (100 * Math.Min(widthScale, heightScale)), height / 2 - (100 * Math.Min(widthScale, heightScale)), 200 * Math.Min(widthScale, heightScale), 190 * Math.Min(widthScale, heightScale));

                g.DrawString("You Lose!", fLarge, Brushes.Black, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-100 * Math.Min(widthScale, heightScale)));
                g.DrawString("You Lose!", fLarge, Brushes.DarkGray, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-98 * Math.Min(widthScale, heightScale)));

                g.DrawString("Restart", f, Brushes.Black, width / 2 - (10 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
                g.DrawString("Back", fSmall, Brushes.Black, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));

                if (MouseHandler.mouseX >= width / 2 - (10 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseX <= width / 2 - (10 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart", f).Width)
                {
                    if (MouseHandler.mouseY >= height / 2 + (50 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= height / 2 + (50 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart", f).Height)
                    {
                        if (MouseHandler.mouseDown) g.DrawString("Restart", f, Brushes.White, width / 2 - (10 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
                        else g.DrawString("Restart", f, Brushes.DarkGray, width / 2 - (10 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
                    }
                }

                if (MouseHandler.mouseX >= width / 2 - (85 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseX <= width / 2 - (85 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Width)
                {
                    if (MouseHandler.mouseY >= height / 2 + (50 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= height / 2 + (50 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Height)
                    {
                        if (MouseHandler.mouseDown) g.DrawString("Back", fSmall, Brushes.White, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
                        else g.DrawString("Back", fSmall, Brushes.DarkGray, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
                    }
                }
            }

            //draw pause menu
            if (Game.subState.Equals(EnumHandler.SubStates.Pause))
            {
                g.DrawImage(pauseIcon, width / 2 - (pauseIcon.Width * widthScale) / 2, height / 2 - (pauseIcon.Height * heightScale) / 2 - (125 * Math.Min(widthScale, heightScale)), pauseIcon.Width * widthScale, pauseIcon.Height * heightScale);

                float heightBase = (height / 2 + (pauseIcon.Height * heightScale) / 2) - (125 * Math.Min(widthScale, heightScale));

                g.DrawString("Restart", f, Brushes.Black, width / 2 - g.MeasureString("Restart", f).Width / 2, heightBase + 50);
                g.DrawString("Return To Main Menu", f, Brushes.Black, width / 2 - g.MeasureString("Return To Main Menu", f).Width / 2, heightBase + 150);
                g.DrawString("Exit To Desktop", f, Brushes.Black, width / 2 - g.MeasureString("Exit To Desktop", f).Width / 2, heightBase + 450);
            }
        }

        //set image frame
        public Boolean SetImageAnimationFrame(Bitmap image, float frame)
        {
            Boolean reset = false;

            if (frame >= image.GetFrameCount(new System.Drawing.Imaging.FrameDimension(image.FrameDimensionsList[0])) - 1)
            {
                reset = true;
            }
            System.Drawing.Imaging.FrameDimension dim = new System.Drawing.Imaging.FrameDimension(image.FrameDimensionsList[0]);
            image.SelectActiveFrame(dim, (int)Math.Round(frame, 0));


            return reset;
        }
    }
}
