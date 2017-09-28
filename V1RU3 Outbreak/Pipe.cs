using System;

namespace V1RU3_Outbreak
{
    public class Pipe
    {
        //define global varaibles
        public int x { get; set; }
        public int y { get; set; }
        public int rotation { get; set; }
        public int type { get; set; }

        //constructor
        public Pipe(int x, int y, int rotation, int type)
        {
            this.x = x;
            this.y = y;
            this.rotation = rotation;
            this.type = type;
        }
    }
}
