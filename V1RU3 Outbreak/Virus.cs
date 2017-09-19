using System;
using System.Collections.Generic;

namespace V1RU3_Outbreak
{
    public class Virus
    {
        //define global variables
        public int x { get; set; }
        public int y { get; set; }
        public float frame { get; set; } = 0;

        //constructor
        public Virus(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
