using System;
using System.Drawing;

namespace V1RU3_Outbreak
{
    public class Particle
    {
        //define global variables
        public float x { get; set; }
        public float y { get; set; }
        public float xVel { get; set; }
        public float yVel { get; set; }
        public float life { get; set; }
        public Color mainColor { get; set; }
        public Color fadeColor { get; set; }
        public float size { get; set; }
        public float rotation { get; set; }

        //constructor
        public Particle(float x, float y, float xVel, float yVel, float life, Color color, Color fadeColor, float size)
        {
            this.x = x;
            this.y = y;
            this.xVel = xVel;
            this.yVel = yVel;
            this.life = life;
            this.mainColor = color;
            this.fadeColor = fadeColor;
            this.size = size;
            this.rotation = Game.r.Next(0, 360);
        }
    }
}
