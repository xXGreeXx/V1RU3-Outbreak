using System;
using System.Collections.Generic;

namespace V1RU3_Outbreak
{
    public class Virus
    {
        //define global variables
        public float x { get; set; }
        public float y { get; set; }
        public float targetX { get; set; } = -1;
        public float targetY { get; set; } = -1;
        public float frame { get; set; } = 0;
        public EnumHandler.VirusTypes type { get; set; }

        //constructor
        public Virus(float x, float y, EnumHandler.VirusTypes type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
    }
}
