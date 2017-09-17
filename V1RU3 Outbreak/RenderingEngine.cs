using System;
using System.Collections.Generic;
using System.Drawing;

namespace V1RU3_Outbreak
{
    public class RenderingEngine
    {
        //define global variables
        Bitmap title = V1RU3_Outbreak.Properties.Resources.title;

        public static float scaleX { get; set; } = 1;
        public static float scaleY { get; set; } = 1;

        public static int canvasWidth { get; set; } = 0;
        public static int canvasHeight { get; set; } = 0;

        private float rotation = 0;

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

            float tileSize = 25 *  Math.Min(widthScale, heightScale);
            float gridSize = 9 * tileSize;

            float tileOffsetX = 0;
            float tileOffsetY = 0;

            g.TranslateTransform((width / 2) + tileOffsetX, (height / 2) + tileOffsetY);
            g.RotateTransform(rotation);
            g.TranslateTransform(-((width / 2) + tileOffsetX), -((height / 2) + tileOffsetY));

            g.FillRectangle(Brushes.White, width / 2 - (gridSize / 2), height / 2 - (gridSize / 2), gridSize, gridSize);

            for (float x = width / 2 - (gridSize / 2); x < width / 2 + (gridSize / 2) - tileSize / 2; x += tileSize)
            {
                for (float  y = height / 2 - (gridSize / 2); y < height / 2 + (gridSize / 2) - tileSize / 2; y += tileSize)
                {
                    g.DrawRectangle(Pens.Black, x, y, tileSize, tileSize);
                }
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
                    g.DrawString("Play", fontForText, Brushes.White, width / 2 - g.MeasureString("Play", fontForText).Width / 2, heightBaseForText + (20 * Math.Min(widthScale, heightScale)));
                }
            }

            if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Options", fontForText).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Options", fontForText).Width / 2)
            {
                if (MouseHandler.mouseY >= heightBaseForText + (60 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= heightBaseForText + (60 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", fontForText).Height)
                {
                    g.DrawString("Options", fontForText, Brushes.White, width / 2 - g.MeasureString("Options", fontForText).Width / 2, heightBaseForText + (60 * Math.Min(widthScale, heightScale)));
                }
            }

            if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Quit", fontForText).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Quit", fontForText).Width / 2)
            {
                if (MouseHandler.mouseY >= heightBaseForText + (150 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= heightBaseForText + (150 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", fontForText).Height)
                {
                    g.DrawString("Quit", fontForText, Brushes.White, width / 2 - g.MeasureString("Quit", fontForText).Width / 2, heightBaseForText + (150 * Math.Min(widthScale, heightScale)));
                }
            }
        }
    }
}
