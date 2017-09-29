using System;
using System.Collections.Generic;

namespace V1RU3_Outbreak
{
    public class Pipes
    {
        //define global variables
        public static List<Pipe> pipes = new List<Pipe>();
        public static int width { get; private set; }
        public static int height { get; private set; }

        //constructor 
        public Pipes()
        {

        }

        //generate level
        public static void GenerateLevel(int width, int height)
        {
            pipes = new List<Pipe>();
            int[] rotations = { 0, 90, 180, 270};

            Pipes.width = width;
            Pipes.height = height;

            for (int i = 0; i < width * height; i++)
            {
                if (i != 0 && i != width * height - 1)
                {
                    pipes.Add(new Pipe(rotations[Game.r.Next(0, rotations.Length)], Game.r.Next(0, 2)));
                }
            }
        }

        //check how well you did on pipes
        public static int CheckPipesWon()
        {
            int percentComplete = 0;

            List<Pipe> path = new List<Pipe>();
            float pipeSize = 10 * Math.Min(RenderingEngine.scaleX, RenderingEngine.scaleY);

            for (int index = 0; index < pipes.Count; index++)
            {
                Pipe p = pipes[index];

                Pipe p2 = null;
                if (index > 0)
                {
                    p2 = pipes[index - 1];
                }

                Pipe p3 = null;
                if (pipes.Count > (index - width / pipeSize))
                {
                    p3 = pipes[(int)(index - width / pipeSize)];
                }
                Pipe p4 = null;
                if (index + 1 < pipes.Count)
                {
                    p4 = pipes[index + 1];
                }

                Pipe p5 = null;
                if (pipes.Count < (index + width / pipeSize))
                {
                    p5 = pipes[(int)(index + width / pipeSize)];
                }

                Boolean[] connects = { pipeConnectsAnotherPipe(p, p2), pipeConnectsAnotherPipe(p, p3), pipeConnectsAnotherPipe(p, p4), pipeConnectsAnotherPipe(p, p5) };

                if (connects[0]) path.Add(p2);
                if (connects[1]) path.Add(p3);
                if (connects[2]) path.Add(p4);
                if (connects[3]) path.Add(p5);
            }


            return percentComplete;
        }

        //check if pipe connects another pipe
        public static Boolean pipeConnectsAnotherPipe(Pipe p1, Pipe p2)
        {
            Boolean connects = false;

            if (p1 == null || p2 == null) return false;


            return connects;
        }
    }
}
