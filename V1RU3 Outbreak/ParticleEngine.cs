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
                g.FillRectangle(new SolidBrush(p.color), p.x, p.y, p.size, p.size);
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

                p.size -= 0.5F;

                int alphaShift = 2;
                if (p.color.A - alphaShift > 0)
                {
                    p.color = Color.FromArgb(p.color.A - alphaShift, p.color);
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
        public void GenerateExplosion(int size, float xBase, float yBase, int amountOfParticles)
        {
            for (int i = 0; i < amountOfParticles; i++)
            {
                float xSpeed = (float)(Math.Cos(i) * Math.PI) + Game.r.Next(-4, 5);
                float ySpeed = (float)(Math.Sin(i) * Math.PI) + Game.r.Next(-4, 5);

                particles.Add(new Particle(xBase, yBase, xSpeed, ySpeed, 25, Color.Red, 20));
            }
        }
    }
}
