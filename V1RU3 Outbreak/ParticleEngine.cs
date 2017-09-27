using System;
using System.Collections.Generic;
using System.Drawing;

namespace V1RU3_Outbreak
{
    public class ParticleEngine
    {
        //define global variables
        private List<Particle> particles = new List<Particle>();

        //constructor
        public ParticleEngine()
        {

        }

        //draw particles
        public void DrawParticles(Graphics g, int width, int height, float widthScale, float heightScale)
        {
            foreach (Particle p in particles)
            {
                if(p.size < 17) g.FillRectangle(new SolidBrush(p.mainColor), p.x, p.y, p.size * Math.Min(widthScale, heightScale), p.size * Math.Min(widthScale, heightScale));
            }
        }

        //simulate particles
        public void SimulateParticles()
        {
            List<int> particlesToRemove = new List<int>();

            foreach (Particle p in particles)
            {
                p.x += p.xVel;
                p.y += p.yVel;

                p.life--;
                if (p.life <= 0) particlesToRemove.Add(particles.IndexOf(p));

                if(p.size > 1) p.size -= 1F;

                int alphaShift = 2;
                if (p.mainColor.A - alphaShift > 0)
                {
                    p.mainColor = Color.FromArgb(p.mainColor.A - alphaShift, p.mainColor);
                }

                int amountToShift = 15;
                //r
                if (p.mainColor.R < p.fadeColor.R && p.mainColor.R + amountToShift < 255)
                {
                    p.mainColor = Color.FromArgb(p.mainColor.A, p.mainColor.R + amountToShift, p.mainColor.G, p.mainColor.B);
                }
                else if (p.mainColor.R > p.fadeColor.R && p.mainColor.R - amountToShift > 0)
                {
                    p.mainColor = Color.FromArgb(p.mainColor.A, p.mainColor.R - amountToShift, p.mainColor.G, p.mainColor.B);
                }

                //g
                if (p.mainColor.G < p.fadeColor.G && p.mainColor.G + amountToShift < 255)
                {
                    p.mainColor = Color.FromArgb(p.mainColor.A, p.mainColor.R, p.mainColor.G + amountToShift, p.mainColor.B);
                }
                else if (p.mainColor.G > p.fadeColor.G && p.mainColor.G - amountToShift > 0)
                {
                    p.mainColor = Color.FromArgb(p.mainColor.A, p.mainColor.R, p.mainColor.G - amountToShift, p.mainColor.B);
                }

                //b
                if (p.mainColor.B < p.fadeColor.B && p.mainColor.B + amountToShift < 255)
                {
                    p.mainColor = Color.FromArgb(p.mainColor.A, p.mainColor.R, p.mainColor.G, p.mainColor.B + amountToShift);
                }
                else if (p.mainColor.B > p.fadeColor.B && p.mainColor.B - amountToShift > 0)
                {
                    p.mainColor = Color.FromArgb(p.mainColor.A, p.mainColor.R, p.mainColor.G, p.mainColor.B - amountToShift);
                }
            }

            particlesToRemove.Sort();
            particlesToRemove.Reverse();

            foreach (int index in particlesToRemove)
            {
                particles.RemoveAt(index);
            }
        }

        //generate explosion
        public void GenerateExplosion(int size, float xBase, float yBase, int amountOfParticles, int width, Color color, Color secondColor)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < amountOfParticles; i++)
            {
                float xSpeed = (float)(Math.Cos(i) * 2.25) + Game.r.Next(-4, 5);
                float ySpeed = (float)(Math.Sin(i) * 2.25) + Game.r.Next(-4, 5);

                particles.Add(new Particle(xBase + x, yBase + y, xSpeed, ySpeed, 20, color, secondColor, size));

                x += 20;
                if (x >= width)
                {
                    x = 0;
                    y += 5;
                }
            }
        }

        //generate fire
        public void GenerateFire(int size, float xBase, float yBase, int amountOfParticles, int width, Color color, Color secondColor)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < amountOfParticles; i++)
            {
                float xSpeed = (float)(Math.Cos(i) * Math.PI) + Game.r.Next(-4, 5);
                float ySpeed = Game.r.Next(-5, -2);

                particles.Add(new Particle(xBase + x, (yBase + Game.r.Next(-1, 2)) + y, xSpeed, ySpeed, 25, color, secondColor, size));

                x += 20;
                if (x >= width)
                {
                    x = 0;
                }
            }
        }
    }
}
