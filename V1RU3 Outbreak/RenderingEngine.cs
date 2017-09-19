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

        public static float scaleX { get; set; } = 1;
        public static float scaleY { get; set; } = 1;

        public static int canvasWidth { get; set; } = 0;
        public static int canvasHeight { get; set; } = 0;

        private float rotation = 0;
        public static int screenFade { get; set; } = 255;

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

            g.TranslateTransform((width / 2) + tileOffsetX, (height / 2) + tileOffsetY);
            g.RotateTransform(-rotation);
            g.TranslateTransform(-((width / 2) + tileOffsetX), -((height / 2) + tileOffsetY));

            //draw title
            float widthOfTitle = title.Width * widthScale;
            g.DrawImage(title, width / 2 - widthOfTitle / 2, 10, title.Width * widthScale, title.Height * heightScale);

            //draw rectangle for options
            g.DrawImage(background, width / 2 - (200 * Math.Min(widthScale, heightScale)) / 2, height / 2 - (200 * Math.Min(widthScale, heightScale)) / 2, 200 * Math.Min(widthScale, heightScale), 200 * Math.Min(widthScale, heightScale));
        }

        //draw game
        public void DrawGame(Graphics g, int width, int height, float widthScale, float heightScale, LevelData level)
        {
            scaleX = widthScale;
            scaleY = heightScale;

            canvasWidth = width;
            canvasHeight = height;

            //define variables
            float tileSize = 15 * Math.Min(widthScale, heightScale);
            float baseX = width / 2 - (level.gridSize * tileSize) / 2;
            float baseY = height / 2 - (level.gridSize * tileSize) / 2;
            Font f = new Font(FontFamily.GenericSansSerif, 15 * Math.Min(widthScale, heightScale), FontStyle.Bold);
            Font fSmall = new Font(FontFamily.GenericSansSerif, 12 * Math.Min(widthScale, heightScale), FontStyle.Bold);
            Font fLarge = new Font(FontFamily.GenericSansSerif, 25 * Math.Min(widthScale, heightScale), FontStyle.Bold);

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

            //draw base grid of level
            for (float x = baseX; x < baseX + level.gridSize * tileSize - tileSize / 2; x += tileSize)
            {
                for (float y = baseY; y < baseY + level.gridSize * tileSize - tileSize / 2; y += tileSize)
                {
                    g.DrawRectangle(Pens.Black, x, y, tileSize, tileSize);
                }
            }

            //draw fade
            int fadeOffs = 2;
            if (screenFade - fadeOffs > 0)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(screenFade, Color.Black));
                g.FillRectangle(brush, 0, 0, width, height);
                screenFade -= fadeOffs;
            }

            //draw win screen
            if (Game.winScreen)
            {
                g.DrawImage(background, width / 2 - (100 * Math.Min(widthScale, heightScale)), height / 2 - (100 * Math.Min(widthScale, heightScale)), 200 * Math.Min(widthScale, heightScale), 190 * Math.Min(widthScale, heightScale));
                g.DrawString("Complete!", fLarge, Brushes.Black, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-100 * Math.Min(widthScale, heightScale)));
                g.DrawString("Complete!", fLarge, Brushes.DarkGray, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-98 * Math.Min(widthScale, heightScale)));

                g.DrawString("Turns Used: " + Game.turnsUsed, fSmall, Brushes.Black, width / 2 - (83 * Math.Min(widthScale, heightScale)), height / 2 + (-50 * Math.Min(widthScale, heightScale)));
                g.DrawString("Viruses: " + Game.levelData.viruses.Count, fSmall, Brushes.Black, width / 2 - (83 * Math.Min(widthScale, heightScale)), height / 2 + (-25 * Math.Min(widthScale, heightScale)));

                g.DrawString("Next Level ->", f, Brushes.Black, width / 2 - (40 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
                g.DrawString("Back", fSmall, Brushes.Black, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));

                if (MouseHandler.mouseX >= width / 2 - (40 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseX <= width / 2 - (40 * Math.Min(widthScale, heightScale)) + g.MeasureString("Next Level ->", fSmall).Width)
                {
                    if (MouseHandler.mouseY >= height / 2 + (50 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= height / 2 + (50 * Math.Min(widthScale, heightScale)) + g.MeasureString("Next Level ->", fSmall).Height)
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

            //draw pause menu
            if (Game.inPause)
            {
                g.FillRectangle(Brushes.Black, width / 2 - (125 * Math.Min(widthScale, heightScale)), height / 2 - (125 * Math.Min(widthScale, heightScale)), 250 * Math.Min(widthScale, heightScale), 250 * Math.Min(widthScale, heightScale));
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
