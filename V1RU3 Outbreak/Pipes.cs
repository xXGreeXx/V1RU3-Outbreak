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


            return percentComplete;
        }

        public static Boolean CheckIfPipeConnected(Pipe p)
        {
            Boolean connected = false;
            int pipeIndex = pipes.IndexOf(p);

            //check if connected to base
            if (pipeIndex == 0 || pipeIndex == width - 1)
            {
                connected = true;
            }

            //check if connected to another pipe
            Pipe p2 = null;
            if (pipeIndex > 0)
            {
                p2 = pipes[pipeIndex - 1];
                if (p2.connected)
                {
                    connected = pipeConnectsAnotherPipe(p, p2);
                }
            }

            Pipe p3 = null;
            if (pipes.Count > (pipeIndex - width) && (pipeIndex - width) > 0)
            {
                p3 = pipes[(int)(pipeIndex - width)];
                if (p3.connected)
                {
                    connected = pipeConnectsAnotherPipe(p, p3);
                }
            }
            Pipe p4 = null;
            if (pipeIndex + 1 < pipes.Count)
            {
                p4 = pipes[pipeIndex + 1];
                if (p4.connected)
                {
                    connected = pipeConnectsAnotherPipe(p, p4);
                }
            }

            Pipe p5 = null;
            if (pipes.Count > (pipeIndex + width))
            {
                p5 = pipes[(int)(pipeIndex + width)];
                if (p5.connected)
                {
                    connected = pipeConnectsAnotherPipe(p, p5);
                }
            }

            return connected;
        }

        //check if pipe connects another pipe
        private static Boolean pipeConnectsAnotherPipe(Pipe p1, Pipe p2)
        {
            Boolean connects = false;

            if (p1 == null || p2 == null) return false;

            //pipe 1, type 0
            if (p1.type == 0)
            {
                //pipe 2, type 0
                if (p2.type == 0)
                {
                    if ((p1.rotation == 0 && p2.rotation == 180) || (p1.rotation == 180 && p2.rotation == 0))
                    {
                        connects = true;
                    }

                    if ((p1.rotation == 90 && p2.rotation == 270) || (p1.rotation == 270 && p2.rotation == 90))
                    {
                        connects = true;
                    }
                }

                //pipe 2, type 1
                if (p2.type == 1)
                {
                    if ((p1.rotation == 0 && (p2.rotation == 180 || p2.rotation == 270)) || (p1.rotation == 180 && (p2.rotation == 180 || p2.rotation == 270)))
                    {
                        connects = true;
                    }
                }
            }

            //pipe 1, type 1
            if (p1.type == 1)
            {
                //pipe 2, type 0
                if (p2.type == 0)
                {

                }

                //pipe 2, type 1
                if (p2.type == 1)
                {

                }
            }

            return connects;
        }
    }
}
