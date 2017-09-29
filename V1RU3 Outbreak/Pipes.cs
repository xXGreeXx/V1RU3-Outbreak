using System;
using System.Collections.Generic;

namespace V1RU3_Outbreak
{
    public class Pipes
    {
        //define global variables
        public static List<Pipe> pipes = new List<Pipe>();

        //constructor 
        public Pipes()
        {

        }

        //generate level
        public static void GenerateLevel(int width, int height)
        {
            pipes = new List<Pipe>();
            int[] rotations = { 0, 90, 180, 270};

            for (int i = 0; i < width * height; i++)
            {
                pipes.Add(new Pipe(rotations[Game.r.Next(0, rotations.Length)], Game.r.Next(0, 2)));
            }
        }
    }
}
