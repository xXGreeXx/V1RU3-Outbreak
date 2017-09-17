using System;
using System.Collections.Generic;
using System.Drawing;

namespace V1RU3_Outbreak
{
    public class RenderingEngine
    {
        //define global variables
        Bitmap title = V1RU3_Outbreak.Properties.Resources.title;

        //constructor
        public RenderingEngine()
        {

        }

        //draw main menu
        public void DrawMainMenu(Graphics g, int width, int height, float widthScale, float heightScale)
        {
            //draw title
            float widthOfTitle = title.Width * widthScale;
            g.DrawImage(title, width / 2 - widthOfTitle / 2, 10, title.Width * widthScale, title.Height * heightScale);

            //draw text
            Font fontForText = new Font(FontFamily.GenericSansSerif, 15 * Math.Min(widthScale, heightScale), FontStyle.Bold);

            float heightBaseForText = title.Height * heightScale + 30;
            g.DrawString("Play", fontForText, Brushes.Black, width / 2 - g.MeasureString("Play", fontForText).Width / 2, heightBaseForText + 20);
            g.DrawString("Options", fontForText, Brushes.Black, width / 2 - g.MeasureString("Options", fontForText).Width / 2, heightBaseForText + 90);
            g.DrawString("Quit", fontForText, Brushes.Black, width / 2 - g.MeasureString("Quit", fontForText).Width / 2, heightBaseForText + 430);
        }
    }
}
