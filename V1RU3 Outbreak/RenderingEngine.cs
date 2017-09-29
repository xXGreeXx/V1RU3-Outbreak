using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

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
        Bitmap partitionAlpha;
        Bitmap importantData = V1RU3_Outbreak.Properties.Resources.importantData;
        Bitmap pauseIcon = V1RU3_Outbreak.Properties.Resources.pauseIcon;
        Bitmap HUD1 = V1RU3_Outbreak.Properties.Resources.HUD1;
        Bitmap HUD2 = V1RU3_Outbreak.Properties.Resources.HUD2;
        Bitmap buttonBack = V1RU3_Outbreak.Properties.Resources.buttonBack;
        Bitmap buttonBackH = V1RU3_Outbreak.Properties.Resources.buttonBackH;
        Bitmap buttonBackC = V1RU3_Outbreak.Properties.Resources.buttonBackC;
        Bitmap box0 = V1RU3_Outbreak.Properties.Resources.box0;
        Bitmap box1 = V1RU3_Outbreak.Properties.Resources.box1;
        Bitmap pipe0 = V1RU3_Outbreak.Properties.Resources.pipe0;
        Bitmap pipe1 = V1RU3_Outbreak.Properties.Resources.pipe1;
        Bitmap pipe2 = V1RU3_Outbreak.Properties.Resources.pipe2;

        public static float scaleX { get; set; } = 1;
        public static float scaleY { get; set; } = 1;

        public static int canvasWidth { get; set; } = 0;
        public static int canvasHeight { get; set; } = 0;

        private float rotation = 0;
        public static int screenFade { get; set; } = 255;

        public static List<String> textOnScreen { get; set; } = new List<String>();
        public static int textAddCycle { get; set; } = 0;
        public static int menuDropInCycle { get; set; } = 0;

        //constructor
        public RenderingEngine()
        {
            partitionAlpha = (Bitmap)SetOpacity(partition, 0.7F);
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
            if (Game.fullscreen) g.DrawImage(box1, width / 2 - (190 * Math.Min(widthScale, heightScale)) / 2 + g.MeasureString("Fullscreen: ", fontForText).Width, height / 2 - (183 * Math.Min(widthScale, heightScale)) / 2, 10 * Math.Min(widthScale, heightScale), 10 * Math.Min(widthScale, heightScale));
            else g.DrawImage(box0, width / 2 - (190 * Math.Min(widthScale, heightScale)) / 2 + g.MeasureString("Fullscreen: ", fontForText).Width, height / 2 - (183 * Math.Min(widthScale, heightScale)) / 2, 10 * Math.Min(widthScale, heightScale), 10 * Math.Min(widthScale, heightScale));
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
            Font fTiny = new Font(FontFamily.GenericMonospace, 5.25F * Math.Min(widthScale, heightScale), FontStyle.Regular | FontStyle.Bold);
            Font fSmall = new Font(FontFamily.GenericMonospace, 12 * Math.Min(widthScale, heightScale), FontStyle.Regular | FontStyle.Bold);
            Font fLarge = new Font(FontFamily.GenericSansSerif, 25 * Math.Min(widthScale, heightScale), FontStyle.Bold | FontStyle.Underline);

            g.DrawImage(board, baseX, baseY, level.gridSize * tileSize, level.gridSize * tileSize);

            //draw block preview
            if (!Game.blockPlaced && Game.subState.Equals(EnumHandler.SubStates.None))
            {
                float xOfPreview = MouseHandler.mouseX - baseX;
                float yOfPreview = MouseHandler.mouseY - baseY;

                xOfPreview /= tileSize;
                xOfPreview = (float)Math.Ceiling(xOfPreview);
                xOfPreview--;

                yOfPreview /= tileSize;
                yOfPreview = (float)Math.Ceiling(yOfPreview);
                yOfPreview--;

                if (xOfPreview > -1 && yOfPreview > -1 && xOfPreview < 20 && yOfPreview < 20)
                {
                    xOfPreview *= tileSize;

                    yOfPreview *= tileSize;

                    g.DrawImage(partitionAlpha, baseX + xOfPreview, baseY + yOfPreview, tileSize, tileSize);
                }
            }

            //draw viruses
            Boolean virusesDoneMoving = true;

            foreach (Virus v in level.viruses)
            {
                v.frame += 0.1F;
                if(SetImageAnimationFrame(virus, v.frame)) v.frame = 0;

                g.DrawImage(virus, baseX + ((v.x - 1) * tileSize), baseY + ((v.y - 1) * tileSize), tileSize, tileSize);

                Boolean moved = Game.MoveVirus(v);
                if (moved) virusesDoneMoving = false;
            }
            if (virusesDoneMoving && !Game.playerTurn)
            {
                Game.CPUcycles = Game.maxCPUCycles;
                Game.playerTurn = true;
                Game.blockPlaced = false;
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

            //draw HUD
            g.DrawImage(HUD1, 0, 0, 30 * widthScale, 300 * heightScale);
            g.DrawImage(HUD2, 30 * widthScale - 3, -1, 450 * widthScale, 25 * heightScale);

            Point[] points = new Point[4];
            points[0] = new Point(10, 10);
            points[1] = new Point((int)(10 + (30 * widthScale - 20)), 10);
            points[2] = new Point((int)(10 + (30 * widthScale - 20)), (int)(300 * heightScale - (50 * heightScale)));
            points[3] = new Point(10, (int)(300 * heightScale - 20));
            g.DrawPolygon(Pens.Black, points);

            int amountToOffset = (int)((300F * heightScale - (50F * heightScale)) * ((float)(Game.CPUcycles - Game.maxCPUCycles) / (float)Game.maxCPUCycles));
            points = new Point[4];
            points[0] = new Point(10, 10 - amountToOffset);
            if (10 - amountToOffset >= 300 * heightScale - (50 * heightScale)) points[1] = new Point((int)(10 + (30 * widthScale - 20)), (int)(300 * heightScale - (50 * heightScale)));
            else points[1] = new Point((int)(10 + (30 * widthScale - 20)), 10 - amountToOffset);
            points[2] = new Point((int)(10 + (30 * widthScale - 20)), (int)(300 * heightScale - (50 * heightScale)));
            points[3] = new Point(10, (int)(300 * heightScale - 20));
            g.FillPolygon(Brushes.Orange, points);

            g.DrawImage(buttonBack, 32 * widthScale - 3, 5 * heightScale, 30 * widthScale, 15 * heightScale);
            if (MouseHandler.mouseX >= 32 * widthScale - 3 && MouseHandler.mouseX <= 32 * widthScale - 3 + (30 * widthScale))
            {
                if (MouseHandler.mouseY >= 5 * heightScale && MouseHandler.mouseY <= 5 * heightScale + (15 * heightScale))
                {
                    if(MouseHandler.mouseDown) g.DrawImage(buttonBackC, 32 * widthScale - 3, 5 * heightScale, 30 * widthScale, 15 * heightScale);
                    else g.DrawImage(buttonBackH, 32 * widthScale - 3, 5 * heightScale, 30 * widthScale, 15 * heightScale);
                }
            }
            g.DrawString("End Turn", fTiny, Brushes.Red, 32.5F * widthScale - 3, 8 * heightScale);


            g.DrawImage(buttonBack, 32 * widthScale + 115, 5 * heightScale, 30 * widthScale, 15 * heightScale);
            if (MouseHandler.mouseX >= 32 * widthScale + 115 && MouseHandler.mouseX <= 32 * widthScale + 115 + (30 * widthScale))
            {
                if (MouseHandler.mouseY >= 5 * heightScale && MouseHandler.mouseY <= 5 * heightScale + (15 * heightScale))
                {
                    if (MouseHandler.mouseDown) g.DrawImage(buttonBackC, 32 * widthScale + 115, 5 * heightScale, 30 * widthScale, 15 * heightScale);
                    else g.DrawImage(buttonBackH, 32 * widthScale + 115, 5 * heightScale, 30 * widthScale, 15 * heightScale);
                }
            }
            g.DrawString("Scan", fTiny, Brushes.Red, 32.5F * widthScale + 128, 8 * heightScale);

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
                menuDropInCycle++;

                if (menuDropInCycle >= 15)
                {
                    g.DrawString("Complete!", fLarge, Brushes.Black, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-100 * Math.Min(widthScale, heightScale)));
                    g.DrawString("Complete!", fLarge, Brushes.DarkGray, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-98 * Math.Min(widthScale, heightScale)));
                    }

                //Game.particleEngine.GenerateExplosion(10, width / 2 - (85 * Math.Min(widthScale, heightScale)) + Game.r.Next(0, 160 * (int)Math.Min(widthScale, heightScale)), height / 2 + (-100 * Math.Min(widthScale, heightScale)), 30, 100, Color.Lime);

                int yOffset = 0;
                foreach (String s in textOnScreen)
                {
                    g.DrawString(s, fSmall, Brushes.Black, width / 2 - (83 * Math.Min(widthScale, heightScale)), height / 2 + (-150 + yOffset * Math.Min(widthScale, heightScale)));
                    yOffset += 20;
                }

                if(textAddCycle < 45 && menuDropInCycle >= 25) textAddCycle++;
                if (textAddCycle == 10)
                {
                    textOnScreen.Add("Turns Used: " + Game.turnsUsed);
                    Game.particleEngine.GenerateExplosion(10, width / 2 - (83 * Math.Min(widthScale, heightScale)), height / 2 + (-150 + 0 * Math.Min(widthScale, heightScale)), 200, 430, Color.Black, Color.Black);
                }
                if (textAddCycle == 25)
                {
                    textOnScreen.Add("Viruses: " + Game.levelData.viruses.Count);
                    Game.particleEngine.GenerateExplosion(10, width / 2 - (83 * Math.Min(widthScale, heightScale)), height / 2 + (-150 + 20 * Math.Min(widthScale, heightScale)), 200, 450, Color.Black, Color.Black);
                }
                if (textAddCycle == 40)
                {
                    textOnScreen.Add("Data Saved: " + Game.levelData.importantData.Count + "/" + new LevelController().levels[Game.levelIndex].importantData.Count);
                    Game.particleEngine.GenerateExplosion(10, width / 2 - (83 * Math.Min(widthScale, heightScale)), height / 2 + (-150 + 40 * Math.Min(widthScale, heightScale)), 200, 500, Color.Black, Color.Black);
                    textAddCycle++;
                }


                if (menuDropInCycle >= 75)
                {
                    g.DrawString("Next Level ->", f, Brushes.Black, width / 2 - (40 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
                    g.DrawString("Back", fSmall, Brushes.Black, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));

                    if (MouseHandler.mouseX >= width / 2 - (40 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseX <= width / 2 - (40 * Math.Min(widthScale, heightScale)) + g.MeasureString("Next Level ->", f).Width)
                    {
                        if (MouseHandler.mouseY >= height / 2 + (50 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= height / 2 + (50 * Math.Min(widthScale, heightScale)) + g.MeasureString("Next Level ->", f).Height)
                        {
                            if (MouseHandler.mouseDown) g.DrawString("Next Level ->", f, Brushes.White, width / 2 - (40 * Math.Min(widthScale, heightScale)), height / 2 + (50 * Math.Min(widthScale, heightScale)));
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
            }

            //draw loss screen
            if (Game.subState.Equals(EnumHandler.SubStates.Loss))
            {
                Game.particleEngine.GenerateFire(10, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-100 * Math.Min(widthScale, heightScale)) + g.MeasureString("You Lose!", fLarge).Height - 20, 30, 500, Color.DarkOrange, Color.Yellow);
                g.DrawString("You Lose!", fLarge, Brushes.Black, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-100 * Math.Min(widthScale, heightScale)));
                g.DrawString("You Lose!", fLarge, Brushes.DarkGray, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-98 * Math.Min(widthScale, heightScale)));

                g.DrawString("Restart", f, Brushes.Black, width / 2 - (10 * Math.Min(widthScale, heightScale)), height / 2 + (-35 * Math.Min(widthScale, heightScale)));
                g.DrawString("Back", fSmall, Brushes.Black, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-35 * Math.Min(widthScale, heightScale)));

                if (MouseHandler.mouseX >= width / 2 - (10 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseX <= width / 2 - (10 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart", f).Width)
                {
                    if (MouseHandler.mouseY >= height / 2 + (-35 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= height / 2 + (-35 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart", f).Height)
                    {
                        if (MouseHandler.mouseDown) g.DrawString("Restart", f, Brushes.White, width / 2 - (10 * Math.Min(widthScale, heightScale)), height / 2 + (-35 * Math.Min(widthScale, heightScale)));
                        else g.DrawString("Restart", f, Brushes.DarkGray, width / 2 - (10 * Math.Min(widthScale, heightScale)), height / 2 + (-35 * Math.Min(widthScale, heightScale)));
                    }
                }

                if (MouseHandler.mouseX >= width / 2 - (85 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseX <= width / 2 - (85 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Width)
                {
                    if (MouseHandler.mouseY >= height / 2 + (-35 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= height / 2 + (-35 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Height)
                    {
                        if (MouseHandler.mouseDown) g.DrawString("Back", fSmall, Brushes.White, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-35 * Math.Min(widthScale, heightScale)));
                        else g.DrawString("Back", fSmall, Brushes.DarkGray, width / 2 - (85 * Math.Min(widthScale, heightScale)), height / 2 + (-35 * Math.Min(widthScale, heightScale)));
                    }
                }
            }

            //draw pause menu
            if (Game.subState.Equals(EnumHandler.SubStates.Pause))
            {
                g.DrawImage(pauseIcon, width / 2 - (100 * widthScale) / 2, height / 2 - (50 * heightScale) / 2 - (125 * Math.Min(widthScale, heightScale)), 100 * widthScale, 50 * heightScale);

                float heightBase = (height / 2 + (50 * heightScale) / 2) - (125 * Math.Min(widthScale, heightScale));

                g.DrawString("Restart", f, Brushes.Black, width / 2 - g.MeasureString("Restart", f).Width / 2, heightBase + (10 * Math.Min(widthScale, heightScale)));
                g.DrawString("Main Menu", f, Brushes.Black, width / 2 - g.MeasureString("Main Menu", f).Width / 2, heightBase + 30 * Math.Min(widthScale, heightScale));
                g.DrawString("Exit To Desktop", f, Brushes.Black, width / 2 - g.MeasureString("Exit To Desktop", f).Width / 2, heightBase + 100 * Math.Min(widthScale, heightScale));

                if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Restart", f).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Restart", f).Width / 2)
                {
                    if (MouseHandler.mouseY >= heightBase + (10 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= heightBase + (10 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart", f).Height)
                    {
                        if(MouseHandler.mouseDown) g.DrawString("Restart", f, Brushes.Gray, width / 2 - g.MeasureString("Restart", f).Width / 2, heightBase + (10 * Math.Min(widthScale, heightScale)));
                        else g.DrawString("Restart", f, Brushes.DarkGray, width / 2 - g.MeasureString("Restart", f).Width / 2, heightBase + (10 * Math.Min(widthScale, heightScale)));
                    }
                }

                if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Main Menu", f).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Main Menu", f).Width / 2)
                {
                    if (MouseHandler.mouseY >= heightBase + 30 * Math.Min(widthScale, heightScale) && MouseHandler.mouseY <= heightBase + 30 * Math.Min(widthScale, heightScale) + g.MeasureString("Main Menu", f).Height)
                    {
                        if (MouseHandler.mouseDown) g.DrawString("Main Menu", f, Brushes.Gray, width / 2 - g.MeasureString("Main Menu", f).Width / 2, heightBase + 30 * Math.Min(widthScale, heightScale));
                        else g.DrawString("Main Menu", f, Brushes.DarkGray, width / 2 - g.MeasureString("Main Menu", f).Width / 2, heightBase + 30 * Math.Min(widthScale, heightScale));
                    }
                }

                if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Exit To Desktop", f).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Exit To Desktop", f).Width / 2)
                {
                    if (MouseHandler.mouseY >= heightBase + 100 * Math.Min(widthScale, heightScale) && MouseHandler.mouseY <= heightBase + 100 * Math.Min(widthScale, heightScale) + g.MeasureString("Exit To Desktop", f).Height)
                    {
                        if (MouseHandler.mouseDown) g.DrawString("Exit To Desktop", f, Brushes.Gray, width / 2 - g.MeasureString("Exit To Desktop", f).Width / 2, heightBase + 100 * Math.Min(widthScale, heightScale));
                        else g.DrawString("Exit To Desktop", f, Brushes.DarkGray, width / 2 - g.MeasureString("Exit To Desktop", f).Width / 2, heightBase + 100 * Math.Min(widthScale, heightScale));
                    }
                }

                PaintVignette(g, new Rectangle(0, 0, width, height));
            }

            //draw puzzles
            if (Game.subState.Equals(EnumHandler.SubStates.Puzzle))
            {
                //pipes
                if (Game.loadedPuzzle.Equals(EnumHandler.PuzzleTypes.Pipes))
                {
                    double timeDifference = Math.Abs(Game.puzzleStart.Subtract(DateTime.Now).TotalSeconds);
                    float timeLeft = (200F * Math.Min(widthScale, heightScale)) * ((float)timeDifference / (float)Game.timeAllowedOnPuzzle);
                    g.DrawRectangle(Pens.Black, width / 2 - (100 * Math.Min(widthScale, heightScale)), height / 2 - (125 * Math.Min(widthScale, heightScale)), 200 * Math.Min(widthScale, heightScale), 30 * Math.Min(widthScale, heightScale));
                    g.FillRectangle(Brushes.Orange, width / 2 - (100 * Math.Min(widthScale, heightScale)), height / 2 - (125 * Math.Min(widthScale, heightScale)), 200 * Math.Min(widthScale, heightScale) - timeLeft, 30 * Math.Min(widthScale, heightScale));

                    g.DrawImage(background, width / 2 - (100 * Math.Min(widthScale, heightScale)), height / 2 - (100 * Math.Min(widthScale, heightScale)), 200 * Math.Min(widthScale, heightScale), 190 * Math.Min(widthScale, heightScale));

                    float pipeSize = 10 * Math.Min(widthScale, heightScale);

                    float xBase = width / 2 - (100 * Math.Min(widthScale, heightScale));
                    float yBase = height / 2 - (100 * Math.Min(widthScale, heightScale));

                    float x = xBase + pipeSize;
                    float y = yBase;

                    g.DrawImage(pipe2, xBase, yBase, pipeSize, pipeSize);
                    foreach (Pipe p in Pipes.pipes)
                    {
                        if (p.type == 0)
                        {
                            if (p.rotation == 0 || p.rotation == 180)
                            {
                                g.DrawImage(pipe0, x, y, pipeSize, pipeSize);
                            }
                            if (p.rotation == 90 || p.rotation == 270)
                            {
                                pipe0.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                g.DrawImage(pipe0, x, y, pipeSize, pipeSize);
                                pipe0.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            }
                        }

                        if (p.type == 1)
                        {
                            switch (p.rotation)
                            {
                                case 0:
                                    g.DrawImage(pipe1, x, y, pipeSize, pipeSize);
                                    break;
                                case 90:
                                    pipe1.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                    g.DrawImage(pipe1, x, y, pipeSize, pipeSize);
                                    pipe1.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    break;
                                case 180:
                                    pipe1.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                    g.DrawImage(pipe1, x, y, pipeSize, pipeSize);
                                    pipe1.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                    break;
                                case 270:
                                    pipe1.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    g.DrawImage(pipe1, x, y, pipeSize, pipeSize);
                                    pipe1.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                    break;
                            }
                        }


                        x += pipeSize;
                        if (x >= width / 2 - (100 * Math.Min(widthScale, heightScale)) + 200 * Math.Min(widthScale, heightScale) - (pipeSize / 2))
                        {
                            x = xBase;
                            y += pipeSize;
                        }
                    }
                    g.DrawImage(pipe2, x, y, pipeSize, pipeSize);

                    //check if lost/won
                    if (Pipes.CheckPipesWon() == 100)
                    {

                    }

                    if (timeDifference >= Game.timeAllowedOnPuzzle)
                    {
                        int percent = Pipes.CheckPipesWon();
                        Game.IsolateViruses(percent);
                    }
                }
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

        //paint vignette
        public void PaintVignette(Graphics g, Rectangle bounds)
        {
            Rectangle ellipsebounds = bounds;
            ellipsebounds.Offset(-ellipsebounds.X, -ellipsebounds.Y);
            int x = ellipsebounds.Width - (int)Math.Round(.70712 * ellipsebounds.Width);
            int y = ellipsebounds.Height - (int)Math.Round(.70712 * ellipsebounds.Height);
            ellipsebounds.Inflate(x, y);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(ellipsebounds);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.WrapMode = WrapMode.Tile;
                    brush.CenterColor = Color.FromArgb(0, 0, 0, 0);
                    brush.SurroundColors = new Color[] { Color.FromArgb(255, 0, 0, 0) };
                    Blend blend = new Blend();
                    blend.Positions = new float[] { 0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0F };
                    blend.Factors = new float[] { 0.0f, 0.5f, 1f, 1f, 1.0f, 1.0f };
                    brush.Blend = blend;
                    Region oldClip = g.Clip;
                    g.Clip = new Region(bounds);
                    g.FillRectangle(brush, ellipsebounds);
                    g.Clip = oldClip;
                }
            }
        }

        //set opacity of image
        public Image SetOpacity(Image image, float opacity)
        {
            var colorMatrix = new ColorMatrix();
            colorMatrix.Matrix33 = opacity;
            var imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(
                colorMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);
            var output = new Bitmap(image.Width, image.Height);
            using (var gfx = Graphics.FromImage(output))
            {
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.DrawImage(
                    image,
                    new Rectangle(0, 0, image.Width, image.Height),
                    0,
                    0,
                    image.Width,
                    image.Height,
                    GraphicsUnit.Pixel,
                    imageAttributes);
            }
            return output;
        }
    }
}
