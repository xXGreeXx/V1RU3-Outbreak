using System;
using System.Collections.Generic;

namespace V1RU3_Outbreak
{
    public class Block
    {
        //define global variables
        public int x { get; set; }
        public int y { get; set; }

        //constructor
        public Block(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
