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
        public Color color { get; set; }
        public float size { get; set; }

        //constructor
        public Particle(float x, float y, float xVel, float yVel, float life, Color color, float size)
        {
            this.x = x;
            this.y = y;
            this.xVel = xVel;
            this.yVel = yVel;
            this.life = life;
            this.color = color;
            this.size = size;
        }
    }
}
